using Backend;
using Backend_DTO.DTOs;
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
    public class ProductionLineTests
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ProductionLineTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(new object[] { "http://localhost:5200/productionline/readall", 28 })]
        public async Task ReadAllProdcutionLines_CorrectTypeAndAmount(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var line = JsonConvert.DeserializeObject<ProductionLineDTO[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, line.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData(new object[] { "http://localhost:5200/productionline/read?productionLine_id=363", 3 })]
        public async Task ReadProductionLine(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            string test = await response.Content.ReadAsStringAsync();
            
            var productionLine = JsonConvert.DeserializeObject<ProductionLineDTO>(test);


            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, productionLine.Machines.Count());
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

    }
}
