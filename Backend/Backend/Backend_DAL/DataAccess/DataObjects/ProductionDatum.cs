using System;
using System.Collections.Generic;

#nullable disable

namespace Backend_DAL.DataAccess.DataObjects
{
    public partial class ProductionDatum
    {
        public int Id { get; set; }
        public int TreeviewId { get; set; }
        public int Treeview2Id { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Port { get; set; }
        public byte Board { get; set; }
    }
}
