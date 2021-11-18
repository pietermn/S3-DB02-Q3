using Backend;
using Backend_Logic.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Backend_Test
{
    public class Test : IClassFixture<AndereTestFactory<Startup>>
    {
        private readonly AndereTestFactory<Startup> _factory;

        public Test(AndereTestFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        //[InlineData(new object[] { "http://localhost:5200/component/readall", typeof(Component), 78 })]
        [InlineData(new object[] { "http://localhost:5200/machine/readall", 65 })]
        //[InlineData(new object[] { "http://localhost:5200/maintenance/readall", typeof(Maintenance), 2 })]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url, int expected)
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





















































        //private readonly HttpClient _client;
        ////private readonly CustomWebApplicationFactory<Startup> _factory;
        //private readonly WebApplicationFactory<Startup> _factory;

        //public Test(CustomWebApplicationFactory<Startup> factory)
        //{
        //    _factory = factory;
        //    _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        //    {
        //        AllowAutoRedirect = false
        //    });
        //}

        //[Fact]
        //public async Task GET_AllMaintenance()
        //{
        //    //Arrange
        //    var httpResponse = await _client.GetAsync("/maintenance/readall");

        //    //Act
        //    httpResponse.EnsureSuccessStatusCode();
        //    //Assert
        //    var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        //    var players = JsonConvert.DeserializeObject<IEnumerable<Maintenance>>(stringResponse);
        //    Assert.Contains(players, p => p.Id == 1);
        //    Assert.Contains(players, p => p.Id == 2);

        //}



        ////VOORBEELD:          Basic Test met WebApplicationFactory
        //[Theory]
        //[InlineData("/")]
        //[InlineData("/Index")]
        //[InlineData("/About")]
        //[InlineData("/Privacy")]
        //[InlineData("/component/readall")]
        //public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await client.GetAsync(url);

        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299
        //    Assert.Equal("text/html; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}


        //////VOORBEELD:          A typical test uses the HttpClient and helper methods to process the request and the response:
        ////[Fact]
        ////public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
        ////{
        ////    // Arrange
        ////    var defaultPage = await _client.GetAsync("/");
        ////    var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

        ////    //Act
        ////    var response = await _client.SendAsync(
        ////        (IHtmlFormElement)content.QuerySelector("form[id='messages']"),
        ////        (IHtmlButtonElement)content.QuerySelector("button[id='deleteAllBtn']"));

        ////    // Assert
        ////    Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
        ////    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        ////    Assert.Equal("/", response.Headers.Location.OriginalString);
        ////}

    }
}
