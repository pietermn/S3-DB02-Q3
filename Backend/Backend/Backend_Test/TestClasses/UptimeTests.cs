using Backend;
using Backend_Logic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend_Test.TestClasses
{
    public class UptimeTests
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UptimeTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(new object[] { "https://localhost:5001/uptime/read?productionLine_id=363", 9 })]
        public async Task ReadUptime(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            string test = await response.Content.ReadAsStringAsync();

            var uptime = JsonConvert.DeserializeObject<Uptime[]>(test);


            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, uptime.Count());
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

    }
}
