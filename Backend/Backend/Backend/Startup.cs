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
using Backend_DAL.DataAccess.DataObjects;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<q3_mms_dbContext>(options => options.UseMySQL("server=localhost;port=3307;user=root;password=root;database=q3_mms_db"));
            services.AddScoped<IComponent, Component>();
            services.AddScoped<IComponentContainer, ComponentContainer>();
            services.AddScoped<IMachine, Machine>();
            services.AddScoped<IMachineContainer, MachineContainer>();
            services.AddScoped<IProductionLine, ProductionLine>();
            services.AddScoped<IProductionLineContainer, ProductionLineContainer>();
            services.AddScoped<IProductionLineHistory, ProductionLineHistory>();
            services.AddScoped<IProductionLineHistoryContainer, ProductionLineHistoryContainer>();
            services.AddScoped<IProductionSide, ProductionSide>();
            services.AddScoped<IProductionSideContainer, ProductionSideContainer>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
