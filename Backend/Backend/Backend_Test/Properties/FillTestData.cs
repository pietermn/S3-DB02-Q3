using Backend_DAL;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Test.Properties
{
    public class FillTestData
    {
        readonly Q3Context _Context;

        public FillTestData(Q3Context context)
        {
            _Context = context;
        }

        public void fillData()
        {
            ComponentDTO c = new() { Id = 1, Name = "Matrijzen", Description = "Test Component", Type = Enums.ComponentType.Coldhalf };
            _Context.Components.Add(c);
            _Context.SaveChanges();
        }
        //var options = new DbContextOptionsBuilder<Q3Context>().UseInMemoryDatabase(databaseName: "InMemoryTestDatabase").Options;
        //var context = new Q3Context(options);
    }
}
