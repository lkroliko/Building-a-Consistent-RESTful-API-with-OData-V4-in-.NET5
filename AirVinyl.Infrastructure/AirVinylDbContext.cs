using AirVinyl.Core.Entities;
using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AirVinyl.Infrastructure
{
    public class AirVinylDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<VinylRecord> VinylRecords { get; set; }
        public DbSet<RecordStore> RecordStores { get; set; }
        public DbSet<PressingDetail> PressingDetails { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<DynamicProperty> DynamicProperties { get; set; }
        public AirVinylDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var vinylRecordsAddedOrModified = ChangeTracker.Entries<VinylRecord>()
                .Where(d => d.State == EntityState.Added || d.State == EntityState.Modified)
                .ToList();

            foreach (var vinylRecord in vinylRecordsAddedOrModified)
            {
                foreach(var property in vinylRecord.Entity.Properties)
                {
                    var dynamicProperty = vinylRecord.Entity.DynamicProperties.FirstOrDefault(d => d.Key == property.Key);
                    if (dynamicProperty == null)
                        vinylRecord.Entity.DynamicProperties.Add(new DynamicProperty() { Key = property.Key, Value = property.Value });
                    else
                        dynamicProperty.Value = property.Value;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
