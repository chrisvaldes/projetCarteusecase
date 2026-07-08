using Blazored.LocalStorage;
using System.Net.Http.Json;
using Use_Case_Carte.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Text.Json;
using Use_Case_Carte.Components.Layout;

namespace Use_Case_Carte.Services
{
    public class ProfilService : BaseApiService
    {
        private readonly IJSRuntime _js;

        private SafeJs _safeJs;

        public ProfilService(HttpClient http, ILocalStorageService storage, IJSRuntime js, SafeJs safeJs) : base(http, storage)
        {
            _js = js;
            _safeJs = safeJs;
        }

        // Retour standardisé : ApiResponse<ProfilModel>
        public async Task<ApiResponse<ProfilModel>> Save(ProfilModel request){
            try {
                await _safeJs.SafeJsUtilities("toggleOnLoaderAndToast");

                await AddAuthHeader();

                var response = await _http.PostAsJsonAsync("api/profil/save", request);

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProfilModel>>();

                if (result == null)
                    throw new Exception("Réponse invalide du serveur.");

                if (result.Success)
                {
                    await _safeJs.SafeJsUtilities("hideCreateProfileModal");
                    await _safeJs.SafeJsUtilities("showToast", result.Message, "success");
                }
                else
                {
                    await _safeJs.SafeJsUtilities("showToast", result.Message, "danger");
                }

                return result;
            }
            finally
            {
                await _safeJs.SafeJsUtilities("toggleOffLoaderAndToast");
            }
        }        

        public async Task Logout()
        {
            await _storage.RemoveItemAsync("authToken");
            await _storage.RemoveItemAsync("refreshToken");
        }

        public async Task<string?> GetToken()
        {
            return await _storage.GetItemAsync<string>("authToken");
        }
    }
}
