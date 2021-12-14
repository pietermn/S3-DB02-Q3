using Backend;
using Backend_DAL;
using Backend_Test.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Test
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<Startup>
    {
        private static DbContextOptions<Q3Context> Create()
        {
            var serviceProvider = new ServiceCollection()
       .AddEntityFrameworkInMemoryDatabase()
       .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<Q3Context>();
            builder.UseInMemoryDatabase("db")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        
        protected override void ConfigureWebHost( IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                //services.AddScoped<IFillTestData, FillTestData>();
                var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Q3Context>));
                //var context = app.ApplicationServices.GetService<Q3Context>();
                
                //services.AddDbContext<Q3Context>(opt => opt.UseInMemoryDatabase("db"));
                //AddTestData(context);
                //IFillTestData.filldata();
                

                services.AddMvc();

                //if (dbContext != null)
                //{
                //    services.Remove(dbContext);
                //}

                //var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                ////services.AddDbContext<Q3Context>(options => options.UseMySQL("server=localhost;port=3307;user=root;password=root;database=db"));
                //services.AddDbContext<Q3Context>(options =>
                //{
                //    options.UseInMemoryDatabase("db");
                //});

                var sp = services.BuildServiceProvider();
                services.AddScoped<Q3Context>();

                //builder.Configure(( IFillTestData fill) => 
                //{
                //    fill.fillData();
                //    //app.UseHttpsRedirection();

                //    app.UseRouting();

                //    app.UseCors(x => x
                //        .AllowAnyMethod()
                //        .AllowAnyHeader()
                //        .AllowAnyOrigin()
                //        .SetIsOriginAllowed(origin => true)
                //     //.AllowCredentials()
                //     );

                //    app.UseAuthorization();

                //    app.UseEndpoints(endpoints =>
                //    {
                //        endpoints.MapControllers();
                //    });
                //});

                using (var context = new Q3Context(Create()))
                {
                        try
                        {
                            context.Database.EnsureCreated();
                            FillTestData fillTestData = new(context);
                            fillTestData.fillData();

                    }
                        catch (Exception ex)
                        {
                            //Log errors
                            throw;
                        }
                    
                }
            });
        }
    }
}
