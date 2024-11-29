using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleWebApi.Models;

namespace SampleWebApi.Repository.Mapping
{
    public class ReportMapping : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("tblReport");
            builder.HasKey(e => e.IdReport);
            builder.Property(e => e.IdReport).ValueGeneratedOnAdd();

            builder.Property(x => x.Deferido).IsRequired();

            builder.HasOne<TipoReport>()
            .WithMany()
            .HasForeignKey(x => x.IdTipoReport)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<FeedbackUsuario>()
            .WithMany()
            .HasForeignKey(x => x.IdFeedbackUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
