using System;
using System.Collections.Generic;
using Backend_Logic_Interface.Models;

namespace Backend_Logic.Models
{
    public class UptimeCollection : IUptimeCollection
    {
        public int ProductionLineId { get; set; }
        public List<IUptime> Uptimes { get; set; }
    }
}
