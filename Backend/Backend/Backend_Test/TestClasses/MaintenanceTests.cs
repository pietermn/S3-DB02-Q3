using Backend;
using Backend_Logic.Models;
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
    public class MaintenanceTests
    : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public MaintenanceTests(CustomWebApplicationFactory<TestStartup> factory)
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
        [InlineData(new object[] { "/maintenance/readall?done=true", 1 })]
        public async Task ReadAllMaintenanceDone_CorrectTypeAndAmount(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var test = JsonConvert.DeserializeObject<Maintenance[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, test.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        
        [Theory]
        [InlineData(new object[] { "/maintenance/readall?done=false", 1 })]
        public async Task ReadAllMaintenanceNotDone_CorrectTypeAndAmount(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var test = JsonConvert.DeserializeObject<Maintenance[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, test.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData(new object[] { "/maintenance/read?component_id=173", 173, "Vervang O-ringen" })]
        public async Task ReadMaintenance(string url,int expectedComponentId, string expectedDescription)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var maintenance = JsonConvert.DeserializeObject<Maintenance>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedComponentId, maintenance.ComponentId);
            Assert.Equal(expectedDescription, maintenance.Description);

        }
    }
}
