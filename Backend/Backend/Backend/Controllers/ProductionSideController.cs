using System.Collections.Generic;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("productionside")]
    public class ProductionSideController : Controller
    {
        readonly IProductionSideContainer _productionSideContainer;
        public ProductionSideController(IProductionSideContainer productionSideContainer)
        {
            _productionSideContainer = productionSideContainer;
        }

        [Route("read"), HttpGet]
        public IActionResult Read(int productionSide_id)
        {
            ProductionSideDTO productionSide = _productionSideContainer.GetProductionSide(productionSide_id);

            return Ok(productionSide);
        }

        [Route("readall"), HttpGet]
        public IActionResult ReadAll()
        {
            List<ProductionSideDTO> productionSide = _productionSideContainer.GetProductionSides();

            return Ok(productionSide);
        }
    }
}
