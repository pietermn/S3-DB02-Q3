//using Backend_Logic.Models;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Backend_Test
//{
//    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
//    {
//        private readonly Component _component = new Component
//        {
//            Id = 1 = new[] {
//            "Playground"
//        },
//            WithReseed = true
//        };

//        protected readonly ApiWebApplicationFactory _factory;
//        protected readonly HttpClient _client;

//        public IntegrationTest(ApiWebApplicationFactory fixture)
//        {
//            _factory = fixture;
//            _client = _factory.CreateClient();

//            _component.Reset(_factory.Configuration.GetConnectionString("SQL")).Wait();
//        }
//    }
//}
