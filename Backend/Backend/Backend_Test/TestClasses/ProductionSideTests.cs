using Backend;
using Backend_DTO.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend_Test.TestClasses
{
    public class ProductionSideTests
: IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public ProductionSideTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("Backend");

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });

            });
        }

        [Theory]
        [InlineData(new object[] { "/productionside/readall", 4 })]
        public async Task ReadAllProdcutionSides_CorrectTypeAndAmount(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var line = JsonConvert.DeserializeObject<ProductionSideDTO[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, line.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData(new object[] { "/productionside/read?productionSide_id=162", "A zijde" })]
        public async Task ReadProductionSide(string url, string expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            string test = await response.Content.ReadAsStringAsync();

            var productionSide = JsonConvert.DeserializeObject<ProductionSideDTO>(test);


            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, productionSide.Name);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

    }
}
