using System;
using System.Linq;
using Backend;
using Backend_DAL;
using Backend_DAL_Interface;
using Backend_Logic.Containers;
using Backend_Logic_Interface.Containers;
using Backend_Test.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend_Test
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Q3Context>();
            services.AddDbContext<Q3Context>(opt => opt.UseInMemoryDatabase("db"));

            services.AddScoped<IComponentContainer, ComponentContainer>();
            services.AddScoped<IMachineContainer, MachineContainer>();
            services.AddScoped<IProductionLineContainer, ProductionLineContainer>();
            services.AddScoped<IProductionLineHistoryContainer, ProductionLineHistoryContainer>();
            services.AddScoped<IProductionSideContainer, ProductionSideContainer>();
            services.AddScoped<IMaintenanceContainer, MaintenanceContainer>();
            services.AddScoped<IUptimeContainer, UptimeContainer>();

            services.AddScoped<IConvertDbDAL, ConvertDatabase>();
            services.AddScoped<IComponentDAL, ComponentDAL>();
            services.AddScoped<IMachineDAL, MachineDAL>();
            services.AddScoped<IProductionLineDAL, ProductionLineDAL>();
            services.AddScoped<IProductionLineHistoryDAL, ProductionLineHistoryDAL>();
            services.AddScoped<IProductionSideDAL, ProductionSideDAL>();
            services.AddScoped<IProductionDAL, ProductionDAL>();
            services.AddScoped<IMaintenanceDAL, MaintenanceDAL>();

            services.AddControllers();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddMvc();
        }


        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, Q3Context context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .SetIsOriginAllowed(origin => true)
             );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Components.RemoveRange(context.Components);
                context.Machines.RemoveRange(context.Machines);
                context.SaveChanges();
                FillTestData fillTestData = new(context);
                fillTestData.fillData();

            }
            catch (Exception ex)
            {
            }
        }


    }
}
