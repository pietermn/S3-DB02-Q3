using Backend_DTO.DTOs;
using System.Collections.Generic;

namespace Backend_DAL_Interface
{
    public interface IProductionLineDAL
    {
        public ProductionLineDTO GetProductionLine(int productLine_id);
        public List<ProductionLineDTO> GetProductionLines();
    }
}
