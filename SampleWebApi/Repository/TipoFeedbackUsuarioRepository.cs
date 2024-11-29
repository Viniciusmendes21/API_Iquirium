using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Repository
{
    public class TipoFeedbackUsuarioRepository : ITipoFeedbackUsuarioRepository
    {
        private readonly SampleContext _dbContext;

        public TipoFeedbackUsuarioRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<TipoFeedbackUsuario>> GetTipoFeedbackUsuario()
        {
            return await _dbContext.TipoFeedbackUsuario.ToListAsync();
        }

        public async Task<TipoFeedbackUsuario> GetTipoFeedbackUsuarioByIdAsync(int id)
        {
            return await _dbContext.TipoFeedbackUsuario.FindAsync(id);
        }

        public async Task AddTipoFeedbackUsuario(TipoFeedbackUsuario tipoFeedbackUsuario)
        {
            await _dbContext.TipoFeedbackUsuario.AddAsync(tipoFeedbackUsuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTipoFeedbackUsuario(TipoFeedbackUsuario tipoFeedbackUsuario)
        {
            _dbContext.TipoFeedbackUsuario.Update(tipoFeedbackUsuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTipoFeedbackUsuario(int id)
        {
            var tipoFeedbackUsuario = await GetTipoFeedbackUsuarioByIdAsync(id);
            if (tipoFeedbackUsuario != null)
            {
                _dbContext.TipoFeedbackUsuario.Remove(tipoFeedbackUsuario);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
