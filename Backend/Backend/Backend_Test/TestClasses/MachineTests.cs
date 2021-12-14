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
    public class MachineTests
     : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public MachineTests(CustomWebApplicationFactory<TestStartup> factory)
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
        [InlineData(new object[] { "/machine/readall", 1 })]
        public async Task ReadAllMachines_CorrectTypeAndAmount(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var test = JsonConvert.DeserializeObject<Machine[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, test.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData(new object[] { "/machine/read?machine_id=1", "Machine", "Machine Description" })]
        public async Task ReadMachine(string url, string expectedName, string expectedDescription)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var machine = JsonConvert.DeserializeObject<Machine>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedName, machine.Name);
            Assert.Equal(expectedDescription, machine.Description);

        }
    }
}
