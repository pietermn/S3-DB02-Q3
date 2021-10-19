using System.Collections.Generic;
using Backend_DTO.DTOs;

namespace Backend_Logic_Interface.Containers
{
    public interface IProductionLineHistoryContainer
    {
        public ProductionLineHistoryDTO GetProductionLineHistory(int productionLineHistory_id);
        public List<ProductionLineHistoryDTO> GetProductionLinesHistory();
    }
}
