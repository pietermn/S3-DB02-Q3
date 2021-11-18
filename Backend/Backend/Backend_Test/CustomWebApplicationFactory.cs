using Backend_DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Test
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        builder.ConfigureServices(services =>
    //        {
    //            var descriptor = services.SingleOrDefault(
    //                d => d.ServiceType ==
    //                    typeof(DbContextOptions<Q3Context>));

    //            services.Remove(descriptor);

    //            services.AddDbContext<Q3Context>(options =>
    //            {
    //                options.UseInMemoryDatabase("InMemoryDbForTesting");
    //            });

    //            var sp = services.BuildServiceProvider();

    //            using (var scope = sp.CreateScope())
    //            {
    //                var scopedServices = scope.ServiceProvider;
    //                var db = scopedServices.GetRequiredService<Q3Context>();
    //                var logger = scopedServices
    //                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

    //                db.Database.EnsureCreated();

    //                try
    //                {
    //                    Utilities.InitializeDbForTests(db);     //DOET HIJ NIETTTTTTTTT
    //                }
    //                catch (Exception ex)
    //                {
    //                    logger.LogError(ex, "An error occurred seeding the " +
    //                        "database with test messages. Error: {Message}", ex.Message);
    //                }
    //            }
    //        });
    //    }












































        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.ConfigureServices(services =>
        //    {
        //        // Create a new service provider.
        //        //var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();                  //<--------------------

        //        // Add a database context (AppDbContext) using an in-memory database for testing.
        //        services.AddDbContext<Q3Context>(options =>
        //        {
        //            //options.UseInMemoryDatabase("InMemoryAppDb");                                                                             //<--------------------
        //            //options.UseInternalServiceProvider(serviceProvider);                                                                      //<--------------------
        //        });

        //        // Build the service provider.
        //        var sp = services.BuildServiceProvider();

        //        // Create a scope to obtain a reference to the database contexts
        //        using (var scope = sp.CreateScope())
        //        {
        //            var scopedServices = scope.ServiceProvider;
        //            var appDb = scopedServices.GetRequiredService<Q3Context>();

        //            var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
        //            // Ensure the database is created.
        //            appDb.Database.EnsureCreated();

        //            try
        //            {
        //                // Seed the database with some specific test data.
        //                //SeedData.PopulateTestData(appDb);                                                                                     //<--------------------
        //            }
        //            catch (Exception ex)
        //            {
        //                logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {ex.Message}");
        //            }
        //        }
        //    });
        //}












        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.ConfigureServices(services =>
        //    {
        //        var descriptor = services.SingleOrDefault(
        //            d => d.ServiceType ==
        //                typeof(DbContextOptions<Q3Context>));

        //        services.Remove(descriptor);

        //        services.AddDbContext<Q3Context>(options =>
        //        {
        //            //options.UseInMemoryDatabase("InMemoryDbForTesting");
        //        });

        //        var sp = services.BuildServiceProvider();

        //        using (var scope = sp.CreateScope())
        //        {
        //            var scopedServices = scope.ServiceProvider;
        //            var db = scopedServices.GetRequiredService<Q3Context>();
        //            var logger = scopedServices
        //                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

        //            db.Database.EnsureCreated();

        //            try
        //            {
        //                //Utilities.InitializeDbForTests(db);
        //            }
        //            catch (Exception ex)
        //            {
        //                logger.LogError(ex, "An error occurred seeding the " +
        //                    "database with test messages. Error: {Message}", ex.Message);
        //            }
        //        }
        //    });
        //}
    }
}
