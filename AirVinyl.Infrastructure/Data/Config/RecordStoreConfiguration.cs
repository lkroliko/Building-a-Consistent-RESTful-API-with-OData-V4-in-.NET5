using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirVinyl.Infrastructure.Data.Config
{
    class RecordStoreConfiguration : IEntityTypeConfiguration<RecordStore>
    {
        public void Configure(EntityTypeBuilder<RecordStore> builder)
        {
            builder.Property(r => r.Name).HasMaxLength(150).IsRequired();

            builder.OwnsOne(r => r.StoreAddress, s =>
            {
                s.Property(a => a.City).HasMaxLength(100);
                s.Property(a => a.Street).HasMaxLength(200);
                s.Property(a => a.PostalCode).HasMaxLength(10);
                s.Property(a => a.Country).HasMaxLength(100);
            });
        }
    }
}
