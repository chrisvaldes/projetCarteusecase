

using API.Application.DTO;

namespace API.Application.Services.IServices
{

    public interface IProfilService
    {
        Task<ApiResponse<ProfilDto>> CreateProfilAsync(ProfilDto profil);
        //Task<ServiceResult<ProfilDto>> UpdateAsync(ProfilDto profilDto);
        //Task<bool> DeleteAsync(Guid id);
        //Task<ServiceResult<Profil>> GetByIdAsync(Guid id);
        //Task<IEnumerable<ProfilDto>> GetAll();
        //public Task<ServiceResult<Profil>> GetByUserag(string userag);

    }
}