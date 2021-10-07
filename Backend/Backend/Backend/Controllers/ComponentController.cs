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
    [Route("component")]
    public class ComponentController : Controller
    {
        readonly IComponentContainer _componentContainer;
        public ComponentController(IComponentContainer componentContainer)
        {
            _componentContainer = componentContainer;
        }

        [Route("read"), HttpGet]
        public IActionResult Read(int component_id)
        {
            ComponentDTO component = _componentContainer.GetComponent(component_id);

            return Ok(component);
        }

        [Route("readall"), HttpGet]
        public IActionResult ReadAll()
        {
            List<ComponentDTO> Components = _componentContainer.GetComponents();

            return Ok(Components);
        }
    }
}
