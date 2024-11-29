using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleWebApi.Models;

namespace SampleWebApi.Repository.Mapping
{
    public class FeedbackUsuarioMapping : IEntityTypeConfiguration<FeedbackUsuario>
    {
        public void Configure(EntityTypeBuilder<FeedbackUsuario> builder)
        {
            builder.ToTable("tblFeedbackUsuario");
            builder.HasKey(e => e.IdFeedbackUsuario);
            builder.Property(e => e.IdFeedbackUsuario).ValueGeneratedOnAdd();

            builder.Property(x => x.StatusFeedback).IsRequired();
            builder.Property(x => x.Mensagem)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.DataAprovacao).IsRequired();

            builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(x => x.IdUsuarioEnvio)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(x => x.IdUsuarioDestino)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(x => x.IdUsuarioIntermedio)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<TipoFeedbackUsuario>()
            .WithMany()
            .HasForeignKey(x => x.IdTipoFeedbackUsuario)
            .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
