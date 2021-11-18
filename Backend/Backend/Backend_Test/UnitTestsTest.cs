using Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend_Test
{
    public class UnitTestsTest : IClassFixture<AndereTestFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UnitTestsTest(AndereTestFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]  
        public async Task Read_GET_Action()
        {
            // Act
            var response = await _client.GetAsync("/maintenance/readall");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            //Assert.Contains("<h1 class=\"bg-info text-white\">Records</h1>", responseString);
        }
    }
}
