using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Containers
{
    public class ProductionLineHistoryContainer : IProductionLineHistoryContainer
    {
        public List<IProductionLineHistory> GetProductionLineHistorys()
        {
            return ConvertToIProductionLineHistory();
        }

        private List<IProductionLineHistory> ConvertToIProductionLineHistory()
        {
            return new List<IProductionLineHistory>();
        }
    }
}
