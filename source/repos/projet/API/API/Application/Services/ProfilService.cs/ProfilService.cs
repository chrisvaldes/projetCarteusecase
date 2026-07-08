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
        private readonly ILogger<ProfilService> _logger;
        public ProfilService(IProfilRepository profilRepository, ILogger<ProfilService> logger)
        {
            _profilRepository = profilRepository;
            _logger = logger;
        }
        public async Task< Profil> CreateProfilAsync(Profil profil)
        {
            try
            {
                var profilResult = await _profilRepository.GetByUseragAsync(profil.Userag!); 

                if (profilResult != null) {
                    _logger.LogWarning("Profil already exists.");
                    return null;
                }

                _logger.LogInformation($"Creating new profile for userag: {profil?.Userag}");
                var created = await _profilRepository.CreateProfilAsync(profil);

                return created;
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur c'est produite lors de la sauvegarde." + ex.Message );
            }
        }
 
    }
}