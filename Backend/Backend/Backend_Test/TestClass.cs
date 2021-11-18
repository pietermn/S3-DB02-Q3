using Backend;
using Backend_Logic.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend_Test
{
    public class TestClass : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly HttpClient _client;

        public TestClass(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        //[Fact]
        //public async Task GET_retrieves_weather_forecast()
        //{
        //    var response = await _client.GetAsync("/component");
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var components = JsonConvert.DeserializeObject<Component[]>(await response.Content.ReadAsStringAsync());
        //    components.Should().HaveCount(5);
        //}
    }
}

