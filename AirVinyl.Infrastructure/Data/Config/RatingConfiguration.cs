using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirVinyl.Infrastructure.Data.Config
{
    class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(r => r.RatedBy);
            builder.HasOne(r => r.RecordStore);

            builder.Property(r => r.Value).IsRequired();
        }
    }
}
