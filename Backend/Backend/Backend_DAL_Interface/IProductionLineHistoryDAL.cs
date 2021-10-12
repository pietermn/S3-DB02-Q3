using Backend_DTO.DTOs;
using System.Collections.Generic;

namespace Backend_DAL_Interface
{
    public interface IProductionLineHistoryDAL
    {
        public ProductionLineHistoryDTO GetProductionLineHistory(int ProductionLineHistory_id);
        public List<ProductionLineHistoryDTO> GetProductionLinesHistory();
    }
}
