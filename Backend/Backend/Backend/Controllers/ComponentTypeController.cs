using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("componenttype")]
    public class ComponentTypeController : Controller
    {
        [Route("read"), HttpGet]
        public IActionResult Read(ComponentType componentType)
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
