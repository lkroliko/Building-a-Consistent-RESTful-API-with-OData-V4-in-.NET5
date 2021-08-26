using AirVinyl.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirVinyl.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static void RegisterInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddDbContext<AirVinylDbContext>(options => options.UseInMemoryDatabase("MemoryDatabase").EnableSensitiveDataLogging().UseLazyLoadingProxies());
            
            services.AddScoped<IRepository, AirVinylRepository>();
        }
    }
}
