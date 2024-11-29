using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Repository
{
    public class FeedbackUsuarioRepository : IFeedbackUsuarioRepository
    {
        private readonly SampleContext _dbContext;

        public FeedbackUsuarioRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<FeedbackUsuario>> GetFeedbackUsuario()
        {
            return await _dbContext.FeedbackUsuario.ToListAsync();
        }

        public async Task<FeedbackUsuario> GetFeedbackUsuarioByIdAsync(int id)
        {
            return await _dbContext.FeedbackUsuario.FindAsync(id);
        }

        public async Task AddFeedbackUsuario(FeedbackUsuario feedbackUsuario)
        {
            feedbackUsuario.DataAprovacao = DateTime.UtcNow;
            await _dbContext.FeedbackUsuario.AddAsync(feedbackUsuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFeedbackUsuario(FeedbackUsuario feedbackUsuario)
        {
            feedbackUsuario.DataAprovacao = DateTime.UtcNow;
            _dbContext.FeedbackUsuario.Update(feedbackUsuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFeedbackUsuario(int id)
        {
            var feedbackUsuario = await GetFeedbackUsuarioByIdAsync(id);
            if (feedbackUsuario != null)
            {
                _dbContext.FeedbackUsuario.Remove(feedbackUsuario);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
