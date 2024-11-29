using SampleWebApi.Models;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.Repository.Interface;


namespace SampleWebApi.Repository
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly SampleContext _dbContext;

        public PerfilRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Perfil>> GetPerfil()
        {
            return await _dbContext.Perfil.ToListAsync();
        }

        public async Task<Perfil> GetPerfilByIdAsync(int id)
        {
            return await _dbContext.Perfil.FindAsync(id);
        }

        public async Task AddPerfil(Perfil perfil)
        {
            await _dbContext.Perfil.AddAsync(perfil);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePerfil(Perfil perfil)
        {
            _dbContext.Perfil.Update(perfil);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePerfil(int id)
        {
            var perfil = await GetPerfilByIdAsync(id);
            if (perfil != null)
            {
                _dbContext.Perfil.Remove(perfil);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
