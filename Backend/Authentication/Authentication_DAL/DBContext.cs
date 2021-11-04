using System;
using Authentication_DTO;
using Microsoft.EntityFrameworkCore;

namespace Authentication_DAL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<SmsReciever> SmsReceivers { get; set; }
    }
}
