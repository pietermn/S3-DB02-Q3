using Backend_DTO.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_DAL
{
    public class Q3Context : DbContext
    {
        public Q3Context(DbContextOptions<Q3Context> options) : base(options) { }

        public DbSet<ComponentDTO> Components { get; set; }
        public DbSet<MachineDTO> Machines { get; set; }
        public DbSet<ProductionLineDTO> ProductionLines { get; set; }
        public DbSet<ProductionLineHistoryDTO> ProductionLinesHistory { get; set; }
        public DbSet<ProductionSideDTO> ProductionSides { get; set; }
        public DbSet<ProductionsDTO> Productions { get; set; }

        
    }
}
