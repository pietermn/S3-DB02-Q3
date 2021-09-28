using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class ProductionLineContainer: IProductionLineContainer
    {
        public IProductionLine GetProductionLine()
        {
            return ConvertToIProductionLine();
        }

        private IProductionLine ConvertToIProductionLine()
        {
            return new ProductionLine(1, "", "", true, 1, 1);
        }
    }
}
