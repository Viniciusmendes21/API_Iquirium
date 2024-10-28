using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
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
