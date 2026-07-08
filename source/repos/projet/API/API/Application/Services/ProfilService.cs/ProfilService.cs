using API.Application.DTO;
using API.Domain.Entities.Enum; 
using API.Domain.Entities; 
using API.Application.Repository.IRepository;
using API.Application.Services.IServices;


namespace API.Application.Service.ProfilService
{

    public class ProfilService : IProfilService
    {
        private readonly IProfilRepository _profilRepository;
        public ProfilService(IProfilRepository profilRepository)
        {
            _profilRepository = profilRepository;
        }
        public async Task<ApiResponse<ProfilDto>> CreateProfilAsync(ProfilDto profilDto)
        {
            try
            {
                var profil = new Profil
                {
                    Id = Guid.NewGuid(),
                    UserName = profilDto.UserName,
                    Userag = profilDto.Userag,
                    Email = profilDto.Email,
                    TypeProfile = profilDto.TypeProfile,
                    Status = EnumStatut.ACTIF.ToString(),
                    IsDeleted = false
                };
                await _profilRepository.CreateProfilAsync(profil);
                return ApiResponse<ProfilDto>.SuccessResponse(profilDto, "Profil created successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProfilDto>.Fail("An error occurred while creating the profile.", new List<string> { ex.Message });
            }
        }
        // Additional methods for updating, deleting, and retrieving profiles can be added here.
    }
}