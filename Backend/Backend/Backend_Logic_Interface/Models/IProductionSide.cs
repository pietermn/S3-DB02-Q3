using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Models
{
    public interface IProductionSide
    {
        public int Id { get; }
        public string Name { get; }
        public List<IProductionLine> ProductionLine { get; }
    }
}
