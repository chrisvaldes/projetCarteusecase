using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.Profils;

public partial class UpdateProfil
{
    [Inject]
    public ProfilService ProfilService { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public ProfilModel Profil { get; set; } = new();

    [Parameter]
    public EventCallback OnSaved { get; set; }

    private async Task SaveProfil()
    {
        var resp = await ProfilService.Save(Profil);

        if (resp?.Success == true)
        {
            await JS.InvokeVoidAsync("hideUpdateProfileModal");

            await OnSaved.InvokeAsync();
        }
    }
}