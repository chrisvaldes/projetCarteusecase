using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;
using System.Text.Json; // ajouté
using Microsoft.JSInterop;

namespace Use_Case_Carte.Components.Pages.Profils;

public partial class Profil : ComponentBase
{
    [Inject]
    private NavigationService NavigationService { get; set; } = default!;

    [Inject]
    protected ProfilService ProfilService { get; set; } = default!;

    protected ProfilModel profilModel = new ();

    [Inject] 
    private IJSRuntime JS { get; set; } = default!; 

    protected override void OnInitialized()
    {
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("initProfileModals");
        }
    }

    // Handler conseillé : reçoit EditContext, retourne Task
    private async Task saveProfil()
    {
        var resp = await ProfilService.Save(profilModel);

        if (resp?.Success == true)
        { 
            profilModel = new ();
            StateHasChanged();  
        }
    }
}