using API_Iquirium.Models;

namespace API_Iquirium.Repositories.Interface
{
    public interface IFeedbackProdutoRepository
    {
        Task<IEnumerable<FeedbackProduto>> GetFeedbackProduto();
        Task<FeedbackProduto> GetFeedbackProdutoByIdAsync(int id);
        Task AddFeedbackProduto(FeedbackProduto feedbackProduto);
        Task UpdateFeedbackProduto(FeedbackProduto feedbackProduto);
        Task DeleteFeedbackProduto(int id);
    }
}
