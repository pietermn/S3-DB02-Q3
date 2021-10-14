using System.Collections.Generic;
using Backend_DTO.DTOs;

namespace Backend_Logic_Interface.Containers
{
    public interface IProductionSideContainer
    {
        public ProductionSideDTO GetProductionSide(int productionSide_id);
        public List<ProductionSideDTO> GetProductionSides();
    }
}
