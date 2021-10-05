using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class ProductionSideContainer: IProductionSideContainer
    {
        public IProductionSide GetProductionSide()
        {
            return ConvertToIProdcutionSide();
        }

        private IProductionSide ConvertToIProdcutionSide()
        {
            return new ProductionSide(1, "");
        }
    }
}
