using System.Collections.Generic;
using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;

namespace Backend_Logic.Containers
{
    public class ProductionLineContainer: IProductionLineContainer
    {
        readonly IProductionLineDAL _productionLineDAL;
        public ProductionLineContainer(IProductionLineDAL productionLineDAL)
        {
            _productionLineDAL = productionLineDAL;
        }

        public ProductionLineDTO GetProductionLine(int productLine_id)
        {
            return _productionLineDAL.GetProductionLine(productLine_id);
        }

        public List<ProductionLineDTO> GetProductionLines()
        {
            return _productionLineDAL.GetProductionLines();
        }
    }
}
