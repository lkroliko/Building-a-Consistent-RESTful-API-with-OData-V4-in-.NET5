using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirVinyl.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using AirVinyl.Entities;
using System.Reflection;
using System.Text.Json.Serialization;

namespace AirVinyl.WebApi
{
    public class Startup
    {
        private readonly bool _isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterInfrastructureDependencies(_isDevelopment, false);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers()
                .AddOData(setup =>
                    {
                        setup.Select().Filter().Expand().Count().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel());
                    })
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AirVinyl.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirVinyl.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.Namespace = "AirVinyl";
            builder.ContainerName = "AirVinylContainer";

            builder.EntitySet<Person>("People");
            builder.EntityType<Person>().ContainsMany(p => p.VinylRecords);
            builder.EntityType<Person>().ContainsMany(p => p.Friends);

            builder.EntitySet<RecordStore>("RecordStores");
            builder.EntityType<RecordStore>().ContainsMany(r => r.Ratings);

            var isHightRatedFunction = builder.EntityType<RecordStore>().Function("IsHightRated");
            isHightRatedFunction.Returns<bool>();
            isHightRatedFunction.Parameter<int>("minimumRating");
            isHightRatedFunction.Namespace = "AirVinyl.Functions";

            var areRatedByFunction = builder.EntityType<RecordStore>().Collection.Function("AreRatedBy");
            areRatedByFunction.ReturnsCollectionFromEntitySet<RecordStore>("RecordStores");
            areRatedByFunction.CollectionParameter<int>("personIds");
            areRatedByFunction.Namespace = "AirVinyl.Functions";

            var getHighRatedRecordStores = builder.Function("GetHighRatedRecordStores");
            getHighRatedRecordStores.ReturnsCollectionFromEntitySet<RecordStore>("RecordStores");
            getHighRatedRecordStores.Parameter<int>("minimumRating");
            getHighRatedRecordStores.Namespace = "AirVinyl.Functions";

            var rateAction = builder.EntityType<RecordStore>().Action("Rate");
            rateAction.Returns<bool>();
            rateAction.Parameter<int>("rating");
            rateAction.Parameter<int>("personId");
            rateAction.Namespace = "AirVinyl.Actions";

            return builder.GetEdmModel();
        }
    }
}
