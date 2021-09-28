using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Models
{
    public class ProductionSide: IProductionSide
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<IProductionLine> ProductionLines { get; set; }

        public ProductionSide(int id, string description)
        {
            Id = id;
            Name = description;
        }
        public ProductionSide(int id, string description, List<IProductionLine> productionLines)
        {
            Id = id;
            Name = description;
            ProductionLines = productionLines;
        }
    }
}
