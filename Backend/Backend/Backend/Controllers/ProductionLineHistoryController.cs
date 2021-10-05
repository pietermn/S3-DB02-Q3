using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("productionlinehistory")]
    public class ProductionLineHistoryController : Controller
    {
        [Route("read"), HttpGet]
        public IActionResult Read(ProductionLineHistory productionLineHistory)
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
