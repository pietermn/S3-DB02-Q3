using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class ProductionlineHistoryContainer
    {
        public List<IProductionLineHistory> GetProductionLineHistorys()
        {
            return new List<IProductionLineHistory>();
        }
    }
}
