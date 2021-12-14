using Backend;
using Backend_DAL;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Test.Properties;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend_Test.TestClasses
{
    public class ComponentTests
    : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public ComponentTests(CustomWebApplicationFactory<TestStartup> factory)
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


        //READALL
        [Theory]
        [InlineData(new object[] { "/component/readall", 1 })]
        public async Task ReadAllComponents_CorrectTypeAndAmount(string url, int expected)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act.
            HttpResponseMessage response = await client.GetAsync(url);
            var test = JsonConvert.DeserializeObject<ComponentDTO[]>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expected, test.Length);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //READ(id)
        [Theory]
        [InlineData(new object[] { "/component/read?component_id=1", "Matrijzen", "Test Component" })]
        public async Task ReadComponent_CorrectTypeAndProperties(string url, string expectedName, string expectedDescription)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var component = JsonConvert.DeserializeObject<ComponentDTO>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedName, component.Name);
            Assert.Equal(expectedDescription, component.Description);

        }

        //GetPreviousActions(id, amount, type)      GEBRUIKER WE DEZE WEL???
        //[Theory]
        //[InlineData(new object[] { "/component/previousactions/173/3/months", new int[]{ 0, 0, 0 } })]
        //public async Task GetPreviousActionsComponent(string url, int[] expected)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();
        //    //int[] expected = new int[]{ 0, 0, 0};

        //    // Act
        //    var response = await client.GetAsync(url);
        //    var test = JsonConvert.DeserializeObject<int[]>(await response.Content.ReadAsStringAsync());

        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299      
        //    Assert.Equal(expected, test);
        //    //Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        //}

        ////SetMaxActions(id, max_actions)                FAILED TO FETCH CODE
        //[Theory]
        //[InlineData(new object[] { "/component/maxactions?component_id=173&max_actions=1000" })]
        //public async Task SetMaxActionsComponent(string url)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await client.GetAsync(url);
            
        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299

        //}
    }
}
