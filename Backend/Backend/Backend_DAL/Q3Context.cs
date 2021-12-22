using Backend_DTO.DTOs;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_DAL
{
    public class Q3Context : DbContext
    {
        public Q3Context () { }

        public Q3Context(DateTime dateTime)
        {
            ProductionDateTime = dateTime;
        }

        public DbSet<ComponentDTO> Components { get; set; }
        public DbSet<MachineDTO> Machines { get; set; }
        public DbSet<ProductionLineDTO> ProductionLines { get; set; }
        public DbSet<ProductionLineHistoryDTO> ProductionLinesHistory { get; set; }
        public DbSet<ProductionSideDTO> ProductionSides { get; set; }
        public DbSet<ProductionsDTO> Productions { get; set; }
        public DbSet<MaintenanceDTO> Maintenance { get; set; }
        public DateTime ProductionDateTime { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Env.TraversePath().Load();
            string connectionString = Env.GetString("ConnectionString");
            string connectionServer = Env.GetString("ConnectionServer");
            string connectionPort = Env.GetString("ConnectionPort");
            optionsBuilder.UseMySQL($"Server={connectionServer};Port={connectionPort};{connectionString}");

            optionsBuilder
                    .ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductionsDTO>(p =>
            {
                p.ToTable($"Productions-{ProductionDateTime.ToString("yyyy-MM")}");
            });


        }
    }
}
