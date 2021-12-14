using Backend;
using Backend.Controllers;
using Backend_DAL;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend_Test.TestClasses
{
    public class ComponentTests2
    {
        [Fact]
        public void GetComponent()
        {
            var builder = new DbContextOptionsBuilder<Q3Context>();
            builder.UseInMemoryDatabase("db");
            var options = builder.Options;

            using (var context = new Q3Context(options))
            {
                var components = new List<ComponentDTO>
                {
                    new() { Id = 1, Name = "Matrijzen", Description = "Test Component", Type = Enums.ComponentType.Coldhalf },
                    new() { Id = 2, Name = "Matrijzen", Description = "Test Component 2", Type = Enums.ComponentType.Hothalf },
                };

                context.AddRange(components);
                //context.AddRange(components.Select(o => o.Id));
                context.SaveChanges();
            }

            using (var context = new Q3Context(options))
            {
                var controller = new ComponentController();
                var test = controller.Read(1);
                //Assert.Equal(1, test.);
            }
        }
    }
}