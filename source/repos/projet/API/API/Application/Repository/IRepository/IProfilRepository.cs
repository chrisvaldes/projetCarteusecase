namespace API.Application.Repository.IRepository
{
    using API.Application.DTO;
    using API.Domain.Entities;
    using API.Domain.Entities.Enum;
    public interface IProfilRepository
    {
        Task<ApiResponse<Profil>> CreateProfilAsync(Profil profilDto);
        Task<ApiResponse<Profil>> UpdateProfilAsync(Guid id, Profil profilDto);
        Task<ApiResponse<Profil>> GetProfilByIdAsync(Guid id);
        Task<ApiResponse<IEnumerable<Profil>>> GetAllProfilsAsync();
        Task<ApiResponse<Profil>> DeleteProfilAsync(Guid id);
        Task<ApiResponse<List<Profil>>> GetProfilsByStatusAsync(EnumStatut status);
    }
}