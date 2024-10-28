using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuario();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task AddUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
    }
}
