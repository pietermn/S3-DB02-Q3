using System;
using System.Collections.Generic;

namespace Backend_Logic_Interface.Models
{
    public interface IUptimeCollection
    {
        public int ProductionLineId { get; set; }
        public List<IUptime> Uptimes { get; set; }
    }
}
