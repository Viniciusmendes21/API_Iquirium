using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
{
    public interface IPerfilRepository
    {
        Task<IEnumerable<Perfil>> GetPerfil();
        Task<Perfil> GetPerfilByIdAsync(int id);
        Task AddPerfil(Perfil perfil);
        Task UpdatePerfil(Perfil perfil);
        Task DeletePerfil(int id);
    }
}
