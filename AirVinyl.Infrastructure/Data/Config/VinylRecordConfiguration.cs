using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirVinyl.Infrastructure.Data.Config
{
    class VinylRecordConfiguration : IEntityTypeConfiguration<VinylRecord>
    {
        public void Configure(EntityTypeBuilder<VinylRecord> builder)
        {
            builder.HasOne(v => v.Person);

            builder.Property(v => v.Title).HasMaxLength(150).IsRequired();
            builder.Property(v => v.Artist).HasMaxLength(150).IsRequired();
            builder.Property(v => v.CatalogNumber).HasMaxLength(50);
            builder.Ignore(v => v.Properties);

            builder.HasMany(v => v.DynamicProperties);
        }
    }
}
