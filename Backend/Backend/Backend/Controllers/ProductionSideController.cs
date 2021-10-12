using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("productionside")]
    public class ProductionSideController : Controller
    {
        [Route("read"), HttpGet]
        public IActionResult Read(ProductionSide productionSide)
        {
            return StatusCode(501);
        }

        [Route("readall"), HttpGet]
        public IActionResult ReadAll()
        {
            return StatusCode(501);
        }
    }
}
