using System.Collections.Generic;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("productionlinehistory")]
    public class ProductionLineHistoryController : Controller
    {
        readonly IProductionLineHistoryContainer _productionLineHistoryContainer;
        public ProductionLineHistoryController(IProductionLineHistoryContainer productionLineHistoryContainer)
        {
            _productionLineHistoryContainer = productionLineHistoryContainer;
        }

        [Route("read"), HttpGet]
        public IActionResult Read(int productionLineHistory_id)
        {
            ProductionLineHistoryDTO plh = _productionLineHistoryContainer.GetProductionLineHistory(productionLineHistory_id);

            return Ok(plh);
        }

        [Route("readall"), HttpGet]
        public IActionResult ReadAll()
        {
            List<ProductionLineHistoryDTO> plh = _productionLineHistoryContainer.GetProductionLinesHistory();

            return Ok(plh);
        }
    }
}
