using Backend_Logic.Models;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class ProductionLineContainer
    {
        public IProductionLine GetProductionLine()
        {
            return new ProductionLine(1, "", "", true, 1, 1);
        }
    }
}
