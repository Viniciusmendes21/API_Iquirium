using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleWebApi.Models;

namespace SampleWebApi.Repository.Mapping
{
    public class TipoFeedbackUsuarioMapping : IEntityTypeConfiguration<TipoFeedbackUsuario>
    {
        public void Configure(EntityTypeBuilder<TipoFeedbackUsuario> builder)
        {
            builder.ToTable("tblTipoFeedbackUsuario");
            builder.HasKey(e => e.IdTipoFeedbackUsuario);
            builder.Property(e => e.IdTipoFeedbackUsuario).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired().HasMaxLength(128);


        }
    }
}
