using API.Application.DTO;
using API.Application.Repository.IRepository;
using API.Domain.Entities;
using API.Domain.Entities.Enum;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Repository{

    public class ProfilRepository : IProfilRepository
    {

		protected ApplicationDbContext _dbContext;
		protected ILogger<ProfilRepository> _logger;

		public ProfilRepository(ApplicationDbContext context, ILogger<ProfilRepository> logger)
		{
			_dbContext = context;
			_logger = logger;
		}

        public async Task<ApiResponse<Profil>> CreateProfilAsync(Profil profil)
        {
			await _dbContext.AddAsync(profil);
			await _dbContext.SaveChangesAsync();
            return  ApiResponse<Profil>.SuccessResponse( profil, "Profil enregistré");
        }

        public async Task<ApiResponse<Profil>> DeleteProfilAsync(Guid id)
        {
            // Récupérer l'entité
			var profil = await _dbContext.Profils.FindAsync(id);

			if (profil == null)
			{
				return  ApiResponse<Profil>.Fail("Le profil n'existe pas."); // Rien à supprimer
			}

			 profil.IsDeleted = true;

			_dbContext.Profils.Update(profil);
			await _dbContext.SaveChangesAsync();
			return ApiResponse<Profil>.SuccessResponse(profil, "Profil supprimer.");;
        }

        public async Task<ApiResponse<IEnumerable<Profil>>> GetAllProfilsAsync()
        {
			IEnumerable<Profil> profils = await _dbContext.Profils.Where(profil => profil.IsDeleted != true).AsNoTracking().ToListAsync();
			return   ApiResponse<IEnumerable<Profil>>.SuccessResponse(profils);
        }

        public async Task<ApiResponse<Profil>> GetProfilByIdAsync(Guid id)
        {
            Profil? profil = await _dbContext.Profils.FindAsync(id);

			_logger.LogInformation("user profile : " + profil!.UserName);

			if (profil == null)
			{
				return ApiResponse<Profil>.Fail("Aucun profil trouvé."); // Retourne null si non trouvé
			}


			return ApiResponse<Profil>.SuccessResponse(profil); // Retourne l'objet Profil trouvé
        }

        public Task<ApiResponse<List<Profil>>> GetProfilsByStatusAsync(EnumStatut status)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Profil>> UpdateProfilAsync(Guid id, Profil profil)
        {
            if (id != profil.Id)
			{
				return ApiResponse<Profil>.Fail("Le profil n'existe pas");
			}

			// Marque l'entité comme modifiée
			_dbContext.Entry(profil).State = EntityState.Modified;

			try
			{
				await _dbContext.SaveChangesAsync();
				return ApiResponse<Profil>.SuccessResponse(profil, "Profil mis-à-jour");
			}
			catch (DbUpdateConcurrencyException)
			{
				return ApiResponse<Profil>.Fail("Erreur de mise-à-jour du profil");
			}
        }
    }
}