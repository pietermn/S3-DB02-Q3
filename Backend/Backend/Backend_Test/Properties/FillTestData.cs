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
            _Context.Components.RemoveRange(_Context.Components);
            _Context.Machines.RemoveRange(_Context.Machines);
            _Context.SaveChanges();
            ComponentDTO c = new() { Id = 1, Name = "Matrijzen", Description = "Test Component", Type = Enums.ComponentType.Coldhalf };
            MachineDTO m = new() { Id = 1, Description = "Machine Description", Name = "Machine" };
            ProductionLineDTO pl = new() { Id = 1, Description = "Production line description", Name = "Productionline", Machines = new List<MachineDTO>() { m } };
            MaintenanceDTO maintenance = new() { Id = 1, ComponentId = 1, Description = "Test description", Done = false, TimeDone = new DateTime() };
            MaintenanceDTO maintenance2 = new() { Id = 2, ComponentId = 1, Description = "Test description", Done = true, TimeDone = new DateTime() };
            ProductionLineHistoryDTO historyDTO = new() { Id = 1, Component = c, ProductionLine = pl };
            _Context.ProductionLinesHistory.Add(historyDTO);
            _Context.Maintenance.AddRange(maintenance, maintenance2);
            _Context.SaveChanges();
        }
        
    }
}
