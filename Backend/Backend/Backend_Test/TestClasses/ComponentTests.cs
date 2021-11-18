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
    public class ComponentTests
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ComponentTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(new object[] { "http://localhost:5200/component/readall", 78 })]
        public async Task ReadAllComponents_CorrectTypeAndAmount(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var test = JsonConvert.DeserializeObject<Component[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, test.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [InlineData(new object[] { "http://localhost:5200/component/read?component_id=173", "Matrijzen", "Roerstaaf 112 mm nr. 9" })]
        public async Task ReadComponent_CorrectTypeAndProperties(string url, string expectedName, string expectedDescription)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var component = JsonConvert.DeserializeObject<Component>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedName, component.Name);
            Assert.Equal(expectedDescription, component.Description);

        }


        [Theory]
        [InlineData(new object[] { "http://localhost:5200/component/readall", 78 })]
        public async Task GetPreviousActionsComponent(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var test = JsonConvert.DeserializeObject<Component[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, test.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [InlineData(new object[] { "http://localhost:5200/component/readall", 78 })]
        public async Task SetMaxActionsComponent(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var test = JsonConvert.DeserializeObject<Component[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, test.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }





    }
}
