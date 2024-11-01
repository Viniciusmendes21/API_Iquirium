using SampleWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleWebApi.Repository.Mapping
{
    public class TipoFeedbackProdutoMapping : IEntityTypeConfiguration<TipoFeedbackProduto>
    {
        public void Configure(EntityTypeBuilder<TipoFeedbackProduto> builder)
        {
            builder.ToTable("tblTipoFeedbackProduto");
            builder.HasKey(e => e.IdTipoFeedbackProduto);
            builder.Property(e => e.IdTipoFeedbackProduto).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired().HasMaxLength(128);


        }
    }
}
