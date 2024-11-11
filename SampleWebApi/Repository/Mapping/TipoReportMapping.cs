using SampleWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleWebApi.Repository.Mapping
{
    public class TipoReportMapping : IEntityTypeConfiguration<TipoReport>
    {
        public void Configure(EntityTypeBuilder<TipoReport> builder)
        {
            builder.ToTable("tblTipoReport");
            builder.HasKey(e => e.IdTipoReport);
            builder.Property(e => e.IdTipoReport).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired().HasMaxLength(128);


        }
    }
}
