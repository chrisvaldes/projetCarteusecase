using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.Profils;

public partial class ListProfil : ComponentBase
{
    [Inject]
    protected ProfilService ProfilService { get; set; } = default!;

    

    public IEnumerable<ProfilModel> profils = new List<ProfilModel>();
    

    public string searchQuery = "";

    // protected override async Task OnInitializedAsync(){
        
    // }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadProfils();
        }
    }

    private async Task LoadProfils()
    {
        profils = await ProfilService.GetAllProfils();
        StateHasChanged();
    }

    public ProfilModel SelectedProfil = new();

    private void EditProfil(ProfilModel profil)
    {
        SelectedProfil = new ProfilModel
        {
            Id = profil.Id,
            UserName = profil.UserName,
            Userag = profil.Userag,
            Email = profil.Email,
            TypeProfile = profil.TypeProfile,
            Status = profil.Status,
        };
    }

}