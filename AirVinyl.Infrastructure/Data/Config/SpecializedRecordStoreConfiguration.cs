using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirVinyl.Infrastructure.Data.Config
{
    class SpecializedRecordStoreConfiguration : IEntityTypeConfiguration<SpecializedRecordStore>
    {
        public void Configure(EntityTypeBuilder<SpecializedRecordStore> builder)
        {
            
        }
    }
}
