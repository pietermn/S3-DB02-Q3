using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Models
{
    public class ProductionLine: IProductionLine
    {
        public int Id { get; private set; }
        public string Description { get; private set; }

        public ProductionLine(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
