using Backend_DAL_Interface;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
