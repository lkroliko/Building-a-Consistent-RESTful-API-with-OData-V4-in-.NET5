using AirVinyl.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AirVinyl.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static void RegisterInfrastructureDependencies(this IServiceCollection services, bool isDevelopment, bool useInMemoryDatabase = true)
        {
            services.AddDbContext<AirVinylDbContext>(options =>
            {
                if (useInMemoryDatabase)
                    options.UseInMemoryDatabase("MemoryDatabase");
                else
                    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AirVinyl;Trusted_Connection=True;");
                if (isDevelopment)
                    options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole().AddDebug(); }))
                    .EnableSensitiveDataLogging();
                
                //options.UseLazyLoadingProxies();
            });

            services.AddScoped<IRepository, AirVinylRepository>();
        }
    }
}
