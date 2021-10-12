using Backend_DTO.DTOs;
using System.Collections.Generic;

namespace Backend_DAL_Interface
{
    public interface IProductionSideDAL
    {
        public ProductionSideDTO GetProductionSide(int ProductionSide_id);
        public List<ProductionSideDTO> GetProductionSides();
    }
}
