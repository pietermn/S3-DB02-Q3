using Backend_DTO.DTOs;
using Backend_Logic_Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Logic_Interface.Containers
{
    public interface IProductionLineContainer
    {
        public List<ProductionLineDTO> GetProductionLines();
        public ProductionLineDTO GetProductionLine(int productLine_id);
    }
}
