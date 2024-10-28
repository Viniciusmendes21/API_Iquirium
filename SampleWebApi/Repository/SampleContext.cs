using SampleWebApi.Models;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.Repository.Mapping;

namespace SampleWebApi.Repository
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options) { }

        public DbSet<FeedbackProduto> FeedbackProduto { get; set; }
        public DbSet<TipoFeedbackProduto> TipoFeedbackProduto { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeedbackProdutoMapping());
            modelBuilder.ApplyConfiguration(new TipoFeedbackProdutoMapping());
            modelBuilder.ApplyConfiguration(new PerfilMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }

    }
}
