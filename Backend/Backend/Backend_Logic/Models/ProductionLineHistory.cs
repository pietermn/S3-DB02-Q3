using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic.Models
{
    public class ProductionLineHistory: IProductionLineHistory
    {
        public IProductionLine ProductionLine { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public ProductionLineHistory(IProductionLine productionLine, DateTime startDate, DateTime endDate) 
        {
            ProductionLine = productionLine;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
