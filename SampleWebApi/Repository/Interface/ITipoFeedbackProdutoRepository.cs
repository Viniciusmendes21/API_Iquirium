using SampleWebApi.Models;

namespace SampleWebApi.Repository.Interface
{
    public interface ITipoFeedbackProdutoRepository
    {
        Task<IEnumerable<TipoFeedbackProduto>> GetTipoFeedbackProduto();
        Task<TipoFeedbackProduto> GetTipoFeedbackProdutoByIdAsync(int id);
        Task AddTipoFeedbackProduto(TipoFeedbackProduto tipoFeedbackProduto);
        Task UpdateTipoFeedbackProduto(TipoFeedbackProduto tipoFeedbackProduto);
        Task DeleteTipoFeedbackProduto(int id);

    }
}
