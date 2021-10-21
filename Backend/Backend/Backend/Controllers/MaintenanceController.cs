using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("maintenance")]
    public class MaintenanceController : Controller
    {
        readonly IMaintenanceContainer _maintenanceContainer;
        public MaintenanceController(IMaintenanceContainer maintenanceContainer)
        {
            _maintenanceContainer = maintenanceContainer;
        }

        [Route("read"), HttpGet]
        public IActionResult Read(int component_id)
        {
            MaintenanceDTO component = _maintenanceContainer.GetMaintenance(component_id);

            return Ok(component);
        }
        
        [Route("readall"), HttpGet]
        public IActionResult ReadAll()
        {
            List<MaintenanceDTO> component = _maintenanceContainer.GetAllMaintenance();

            return Ok(component);
        }



    }
}
