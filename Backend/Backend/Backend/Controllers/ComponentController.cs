using System;
using System.Collections.Generic;
using Backend_DTO.DTOs;
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

        //[Route("previousactions/{component_id}/{amount}/{type}"), HttpGet]
        //public IActionResult GetPreviousActions(int component_id, int amount, string type)
        //{
        //    List<int> Actions = _componentContainer.GetPreviousActions(component_id, amount, type);

        //    return Ok(Actions);
        //}

        [Route("previousactions/{component_id}/{beginDate}/{endDate}"), HttpGet]
        public IActionResult GetPreviousActions(int component_id, DateTime beginDate, DateTime endDate)
        {
            List<ProductionsDateDTO> Productions = _componentContainer.GetPreviousActions(component_id, beginDate, endDate);

            return Ok(Productions);
        }

        [Route("maxactions"), HttpPut]
        public IActionResult SetMaxActions(int component_id, int max_actions)
        {
            _componentContainer.SetMaxActions(component_id, max_actions);

            return Ok();
        }
    }
}
