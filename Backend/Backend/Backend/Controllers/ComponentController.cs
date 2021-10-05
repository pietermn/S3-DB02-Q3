using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Logic.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("component")]
    public class ComponentController : Controller
    {
        [Route("read"), HttpGet]
        public IActionResult Read(Component component)
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
