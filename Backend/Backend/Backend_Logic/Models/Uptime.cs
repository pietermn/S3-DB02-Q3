using System;
using Backend_Logic_Interface.Models;

namespace Backend_Logic.Models
{
    public class Uptime : IUptime
    {
        public int Id { get; set; }
        public int ProductionLineId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public bool Active { get; set; }

        public Uptime(int id, int productionLineId, DateTime begin, DateTime end, bool active)
        {
            Id = id;
            ProductionLineId = productionLineId;
            Begin = begin;
            End = end;
            Active = active;
        }
    }
}
