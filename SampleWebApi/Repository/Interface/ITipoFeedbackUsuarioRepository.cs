using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
{
    public interface ITipoFeedbackUsuarioRepository
    {
        Task<IEnumerable<TipoFeedbackUsuario>> GetTipoFeedbackUsuario();
        Task<TipoFeedbackUsuario> GetTipoFeedbackUsuarioByIdAsync(int id);
        Task AddTipoFeedbackUsuario(TipoFeedbackUsuario tipoFeedbackUsuario);
        Task UpdateTipoFeedbackUsuario(TipoFeedbackUsuario tipoFeedbackUsuario);
        Task DeleteTipoFeedbackUsuario(int id);
    }
}
