using Backend_DTO.DTOs;
using System.Collections.Generic;

namespace Backend_DAL_Interface
{
    public interface IProductionDAL
    {
        public List<ProductionsDTO> GetProductionsByIdFromLastDay(int productionLine_id);
        public List<ProductionsDTO> GetProductionsFromLastDay();
        public List<ProductionsDTO> GetAllProductionsFromProductionLine(int productionLine_id);
    }
}
