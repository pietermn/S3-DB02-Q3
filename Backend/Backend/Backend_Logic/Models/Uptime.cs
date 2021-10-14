using System;
using Backend_Logic_Interface.Models;

namespace Backend_Logic.Models
{
    public class Uptime : IUptime
    {
        public int id { get; }
        public int ProductionLineId { get; }
        public DateTime Timestamp { get; }
        public bool active { get; }

        public Uptime(int ID, int productionLineId, DateTime timestamp, bool Active)
        {
            id = ID;
            ProductionLineId = productionLineId;
            Timestamp = timestamp;
            active = Active;
        }
    }
}
