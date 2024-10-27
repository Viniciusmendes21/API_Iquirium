using API_Iquirium.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleWebApi.Repository.Mapping
{
    public class FeedbackProdutoMapping : IEntityTypeConfiguration<FeedbackProduto>
    {
        public void Configure(EntityTypeBuilder<FeedbackProduto> builder)
        {
            builder.ToTable("tblFeedbackProduto");
            builder.HasKey(e => e.IdFeedbackProduto);
            builder.Property(e => e.IdFeedbackProduto).ValueGeneratedOnAdd();

            builder.Property(x => x.Comentario).HasMaxLength(128);
            builder.Property(x => x.DataEnvio).IsRequired();

            builder.HasOne<TipoFeedbackProduto>()
            .WithMany()
            .HasForeignKey(x => x.IdTipoFeedbackProduto)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(x => x.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
