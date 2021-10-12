using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;

namespace Backend_Logic.Containers
{
    public class ProductionSideContainer: IProductionSideContainer
    {
        readonly IProductionSideDAL _productionSideDAL;
        public ProductionSideContainer(IProductionSideDAL productionSideDAL)
        {
            _productionSideDAL = productionSideDAL;
        }

        public ProductionSideDTO GetProductionSide(int productionSide_id)
        {
            return _productionSideDAL.GetProductionSide(productionSide_id);
        }

        public List<ProductionSideDTO> GetProductionSides()
        {
            return _productionSideDAL.GetProductionSides();
        }
    }
}
