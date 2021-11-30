using System.Collections.Generic;
using Backend_DTO.DTOs;
using Backend_Logic_Interface.Containers;
using Backend_Logic_Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("uptime")]
    public class UptimeController : Controller
    {
        readonly IUptimeContainer _uptimeContainer;
        public UptimeController(IUptimeContainer uptimeContainer)
        {
            _uptimeContainer = uptimeContainer;
        }

        [Route("read"), HttpGet]
        public IActionResult Read(int productionLine_id)
        {
            List<IUptime> Uptimes = _uptimeContainer.GetUptimeByIdFromLastDay(productionLine_id);

            return Ok(Uptimes);
        }

        //[Route("readall"), HttpGet]
        //public IActionResult ReadAll()
        //{
        //    List<IUptimeCollection> Uptimes = _uptimeContainer.GetUptimeFromLastDay();

        //    return Ok(Uptimes);
        //}
    }
}
