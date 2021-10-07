using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_DTO.DTOs;
using Backend_Logic.Models;
using Backend_Logic_Interface.Containers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("productionline")]
    public class ProductionLineController : Controller
    {
        readonly IProductionLineContainer _productionLineContainer;
        public ProductionLineController(IProductionLineContainer productionLineContainer)
        {
            _productionLineContainer = productionLineContainer;
        }

        [Route("read"), HttpGet]
        public IActionResult Read(int productionLine_id)
        {
            ProductionLineDTO productionLine = _productionLineContainer.GetProductionLine(productionLine_id);

            return Ok(productionLine);
        }

        [Route("readall"), HttpGet]
        public IActionResult ReadAll()
        {
            List<ProductionLineDTO> productionLines = _productionLineContainer.GetProductionLines();

            return Ok(productionLines);
        }
    }
}
