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
        //public DbSet<FeedbackUsuario> FeedbackUsuario { get; set; }
        public DbSet<TipoReport> TipoReport { get; set; }
        public DbSet<TipoFeedbackUsuario> TipoFeedbackUsuario { get; set; }
        public DbSet<TipoFeedbackProduto> TipoFeedbackProduto { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeedbackProdutoMapping());
            modelBuilder.ApplyConfiguration(new TipoFeedbackProdutoMapping());
            modelBuilder.ApplyConfiguration(new PerfilMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new TipoReportMapping());
            modelBuilder.ApplyConfiguration(new TipoFeedbackUsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }

    }
}
