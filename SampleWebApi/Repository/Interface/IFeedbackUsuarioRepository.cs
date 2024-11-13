using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
{
    public interface IFeedbackUsuarioRepository
    {
        Task<IEnumerable<FeedbackUsuario>> GetFeedbackUsuario();
        Task<FeedbackUsuario> GetFeedbackUsuarioByIdAsync(int id);
        Task AddFeedbackUsuario(FeedbackUsuario feedbackUsuario);
        Task UpdateFeedbackUsuario(FeedbackUsuario feedbackUsuario);
        Task DeleteFeedbackUsuario(int id);
    }
}
