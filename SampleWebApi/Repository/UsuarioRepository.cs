using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;
using SampleWebApi.Repository.Interface;

namespace SampleWebApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SampleContext _dbContext;

        public UsuarioRepository(SampleContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Usuario>> GetUsuario()
        {
            return await _dbContext.Usuario.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _dbContext.Usuario.FindAsync(id);
        }

        public async Task AddUsuario(Usuario usuario)
        {
            await _dbContext.Usuario.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            _dbContext.Usuario.Update(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUsuario(int id)
        {
            var usuario = await GetUsuarioByIdAsync(id);
            if (usuario != null)
            {
                _dbContext.Usuario.Remove(usuario);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
