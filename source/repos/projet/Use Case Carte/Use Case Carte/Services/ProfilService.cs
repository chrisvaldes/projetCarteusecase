using Blazored.LocalStorage;
using System.Net.Http.Json;
using Use_Case_Carte.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Use_Case_Carte.Services
{
    public class ProfilService : BaseApiService
    {
        public ProfilService(HttpClient http, ILocalStorageService storage) : base(http, storage)
        {
        }

        // Retour standardisé : ApiResponse<ProfilModel>
        public async Task<ApiResponse<ProfilModel>> Save(ProfilModel request)
        {
            await AddAuthHeader();

            Console.WriteLine($"===========>>   Profil: {request }");
            Console.WriteLine($"===========>> Save Profil: {request.UserName}, {request.Email}");
            Console.WriteLine($"===========>> base address : {_http.BaseAddress}");

            var response = await _http.PostAsJsonAsync("api/profil/save", request);
            var content = await response.Content.ReadAsStringAsync();

            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Si succès, tenter de désérialiser en ApiResponse<ProfilModel> ou ProfilModel
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var apiResp = JsonSerializer.Deserialize<ApiResponse<ProfilModel>>(content, jsonOptions);
                    if (apiResp != null) return apiResp;
                }
                catch
                {
                    // ignore parsing error, on essaiera l'autre format
                }

                try
                {
                    var model = JsonSerializer.Deserialize<ProfilModel>(content, jsonOptions);
                    return new ApiResponse<ProfilModel>
                    {
                        Success = true,
                        Message = "Profil créé avec succès",
                        Data = model
                    };
                }
                catch
                {
                    return new ApiResponse<ProfilModel>
                    {
                        Success = true,
                        Message = "Profil créé avec succès",
                        Data = null
                    };
                }
            }

            // En cas d'erreur, tenter d'extraire un ApiResponse provenant de l'API
            try
            {
                var errorResp = JsonSerializer.Deserialize<ApiResponse<ProfilModel>>(content, jsonOptions);
                if (errorResp != null) return errorResp;
            }
            catch
            {
                // ignore
            }

            // Fallback : retourner un ApiResponse avec le contenu brut comme message
            return new ApiResponse<ProfilModel>
            {
                Success = false,
                Message = string.IsNullOrWhiteSpace(content) ? "Erreur inconnue" : content,
                Data = null
            };
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
