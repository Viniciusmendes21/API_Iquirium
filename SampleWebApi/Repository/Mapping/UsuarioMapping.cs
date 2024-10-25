using API_Iquirium.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleWebApi.Repository.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("tblUsuario");
            builder.HasKey(e => e.IdUsuario);
            builder.Property(e => e.IdUsuario).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasMaxLength(128).IsRequired();

            builder.HasOne<Perfil>()
                   .WithMany()
                   .HasForeignKey(u => u.IdPerfil)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
