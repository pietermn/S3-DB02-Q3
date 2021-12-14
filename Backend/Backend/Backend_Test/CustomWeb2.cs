using Backend;
using Backend_DAL;
using Backend_DTO.DTOs;
using Backend_Test.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Test
{
    public class CustomWeb2 : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public CustomWeb2()
        {
            Connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True");

            Seed();

            Connection.Open();
        }
        public DbConnection Connection { get; }
        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        ComponentDTO c = new() { Id = 1, Name = "Matrijzen", Description = "Test Component", Type = Enums.ComponentType.Coldhalf };

                        context.AddRange(c);

                        context.SaveChanges();
                    }
                }
            }
        }
        public Q3Context CreateContext(DbTransaction transaction = null)
        {
            var context = new Q3Context(new DbContextOptionsBuilder<Q3Context>().UseSqlServer(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }
        public void Dispose() => Connection.Dispose();
    }
}
