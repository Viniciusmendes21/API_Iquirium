using API_Iquirium.Models;
using Microsoft.EntityFrameworkCore;
using API_Iquirium.Repositories.Interface;
using API_Iquirium.Models;
using SampleWebApi.Repository;

namespace API_Iquirium.Repositories
{
    public class FeedbackProdutoRepository : IFeedbackProdutoRepository
    {
        private readonly SampleContext _dbContext; // Altere para AppDbContext se necessário

        public FeedbackProdutoRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<FeedbackProduto>> GetFeedbackProduto()
        {
            return await _dbContext.FeedbackProduto.ToListAsync(); // Verifique se a propriedade está correta
        }

        public async Task<FeedbackProduto> GetFeedbackProdutoByIdAsync(int id)
        {
            return await _dbContext.FeedbackProduto.FindAsync(id); // Verifique se a propriedade está correta
        }

        public async Task AddFeedbackProduto(FeedbackProduto feedbackProduto)
        {
            feedbackProduto.DataEnvio = DateTime.UtcNow;
            await _dbContext.FeedbackProduto.AddAsync(feedbackProduto); // Verifique se a propriedade está correta
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFeedbackProduto(FeedbackProduto feedbackProduto)
        {
            feedbackProduto.DataEnvio = DateTime.UtcNow;
            _dbContext.FeedbackProduto.Update(feedbackProduto); // Verifique se a propriedade está correta
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFeedbackProduto(int id)
        {
            var feedbackProduto = await GetFeedbackProdutoByIdAsync(id);
            if (feedbackProduto != null)
            {
                _dbContext.FeedbackProduto.Remove(feedbackProduto); // Verifique se a propriedade está correta
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
