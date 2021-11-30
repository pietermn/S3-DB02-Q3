using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IProductionLineHistory
    {
        public int Id { get; }
        public IProductionLine ProductionLine { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }
}
