using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Repository
{
    public class TipoFeedbackProdutoRepository : ITipoFeedbackProdutoRepository
    {
        private readonly SampleContext _dbContext;

        public TipoFeedbackProdutoRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<TipoFeedbackProduto>> GetTipoFeedbackProduto()
        {
            return await _dbContext.TipoFeedbackProduto.ToListAsync();
        }

        public async Task<TipoFeedbackProduto> GetTipoFeedbackProdutoByIdAsync(int id)
        {
            return await _dbContext.TipoFeedbackProduto.FindAsync(id);
        }

        public async Task AddTipoFeedbackProduto(TipoFeedbackProduto tipoFeedbackProduto)
        {
            await _dbContext.TipoFeedbackProduto.AddAsync(tipoFeedbackProduto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTipoFeedbackProduto(TipoFeedbackProduto tipoFeedbackProduto)
        {
            _dbContext.TipoFeedbackProduto.Update(tipoFeedbackProduto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTipoFeedbackProduto(int id)
        {
            var tipoFeedbackProduto = await GetTipoFeedbackProdutoByIdAsync(id);
            if (tipoFeedbackProduto != null)
            {
                _dbContext.TipoFeedbackProduto.Remove(tipoFeedbackProduto);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
