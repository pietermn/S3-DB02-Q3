using System;
namespace Backend_Logic_Interface.Models
{
    public interface IUptime
    {
        public int id { get; }
        public int ProductionLineId { get; }
        public DateTime Timestamp { get; }
        public bool active { get; }
    }
}
