#region

using comrade.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace comrade.Infrastructure.Mappings
{
    public class AirplaneConfiguration : IEntityTypeConfiguration<Airplane>
    {
        public void Configure(EntityTypeBuilder<Airplane> builder)
        {
            builder.Property(b => b.Id).HasColumnName("AIRP_SQ_AIRPLANE").IsRequired();
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Codigo).HasDatabaseName("IX_AIRPLANE_CODIGO").IsUnique();
        }
    }
}