using System;
namespace Backend_Logic_Interface.Models
{
    public interface IUptime
    {
        public int Id { get; }
        public int ProductionLineId { get; }
        public DateTime Begin { get; }
        public DateTime End { get; }
        public bool Active { get; }
    }
}
