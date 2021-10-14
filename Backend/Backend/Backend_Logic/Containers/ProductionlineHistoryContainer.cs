using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;

namespace Backend_Logic.Containers
{
    public class ProductionLineHistoryContainer : IProductionLineHistoryContainer
    {
        readonly IProductionLineHistoryDAL _productionLineHistoryDAL;
        public ProductionLineHistoryContainer(IProductionLineHistoryDAL productionLineHistoryDAL)
        {
            _productionLineHistoryDAL = productionLineHistoryDAL;
        }

        public ProductionLineHistoryDTO GetProductionLineHistory(int productionLineHistory_id)
        {
            return _productionLineHistoryDAL.GetProductionLineHistory(productionLineHistory_id);
        }

        public List<ProductionLineHistoryDTO> GetProductionLinesHistory()
        {
            return _productionLineHistoryDAL.GetProductionLinesHistory();
        }
    }
}
