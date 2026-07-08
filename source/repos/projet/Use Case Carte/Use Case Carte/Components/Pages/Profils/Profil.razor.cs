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
    private async Task saveProfil( )
    {
        // Sérialiser le modèle pour voir toutes les propriétés dans les logs
        var json = JsonSerializer.Serialize(profilModel);
        Console.WriteLine($"===========>> Profil serialized: {json}");
        Console.WriteLine($"===========>> UserName='{profilModel.UserName}' Email='{profilModel.Email}'");

        var resp = await ProfilService.Save(profilModel);
        Console.WriteLine($"===========>> Save response: Success={resp?.Success} Message={resp?.Message}");
    }
}