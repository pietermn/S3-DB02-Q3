using Backend_Logic.Containers;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
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
using Microsoft.EntityFrameworkCore;
using Backend_DAL;
using Backend_DAL_Interface;
using DotNetEnv;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            Env.TraversePath().Load();
            string connectionString = Env.GetString("ConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
            services.AddDbContext<Q3Context>(options => options.UseMySQL("server=localhost;port=3307;user=root;password=root;database=db;SslMode=None"));
            } else
            {
                services.AddDbContext<Q3Context>(options => options.UseMySQL(connectionString));
            }

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
            });

            services.AddCors();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, Q3Context context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .SetIsOriginAllowed(origin => true)
                //.AllowCredentials()
             );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
