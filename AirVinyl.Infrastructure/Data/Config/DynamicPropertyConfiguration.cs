using AirVinyl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirVinyl.Infrastructure.Data.Config
{
    class DynamicPropertyConfiguration : IEntityTypeConfiguration<DynamicProperty>
    {
        public void Configure(EntityTypeBuilder<DynamicProperty> builder)
        {
            builder.Ignore(d => d.Value);
            //builder.HasOne()

            //builder.HasKey(new[] { "Name", "VinylRecordId" });
        }
    }
}
