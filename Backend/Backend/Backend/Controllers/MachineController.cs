using System.Collections.Generic;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [ApiController]
    [Route("machine")]
    public class MachineController : Controller
    {
        readonly IMachineContainer _machineContainer;
        public MachineController(IMachineContainer machineContainer)
        {
            _machineContainer = machineContainer;
        }

        [Route("read"), HttpGet]
        public IActionResult Read(int machine_id)
        {
            MachineDTO machine = _machineContainer.GetMachine(machine_id);

            return Ok(machine);
        }

        [Route("readall"), HttpGet]
        public IActionResult ReadAll()
        {
            List<MachineDTO> machines = _machineContainer.GetMachines();

            return Ok(machines);
        }
    }
}
