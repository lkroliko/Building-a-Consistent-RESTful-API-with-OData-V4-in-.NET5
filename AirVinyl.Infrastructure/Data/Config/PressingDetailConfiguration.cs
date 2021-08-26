using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirVinyl.Infrastructure.Data.Config
{
    class PressingDetailConfiguration : IEntityTypeConfiguration<PressingDetail>
    {
        public void Configure(EntityTypeBuilder<PressingDetail> builder)
        {
            builder.Property(p => p.Grams).IsRequired();
            builder.Property(p => p.Inches).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();
        }
    }
}
