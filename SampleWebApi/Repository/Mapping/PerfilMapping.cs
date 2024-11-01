using SampleWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleWebApi.Repository.Mapping
{
    public class PerfilMapping : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("tblPerfil");
            builder.HasKey(e => e.IdPerfil);
            builder.Property(e => e.IdPerfil).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasMaxLength(128).IsRequired();


        }

    }
}
