using System.Collections.Generic;
using Backend_DTO.DTOs;

namespace Backend_Logic_Interface.Containers
{
    public interface IProductionLineContainer
    {
        public List<ProductionLineDTO> GetProductionLines();
        public ProductionLineDTO GetProductionLine(int productLine_id);
    }
}
