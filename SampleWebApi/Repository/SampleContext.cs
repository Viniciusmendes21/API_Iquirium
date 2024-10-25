using API_Iquirium.Models;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeedbackProdutoMapping());
            modelBuilder.ApplyConfiguration(new TipoFeedbackProdutoMapping());

            base.OnModelCreating(modelBuilder);
        }

    }
}
