using Backend;
using Backend_DAL;
using Backend_Test.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Test
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Q3Context>));

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

                //using (var scope = sp.CreateScope())
                //{
                //    using (var appContext = scope.ServiceProvider.GetRequiredService<Q3Context>())
                //    {
                //        try
                //        {
                //            appContext.Database.EnsureCreated();
                //            //FillTestData fillTestData = new(appContext);
                //            //fillTestData.fillData();

                //        }
                //        catch (Exception ex)
                //        {
                //            //Log errors
                //            throw;
                //        }
                //    }
                //}
            });
        }
    }
}
