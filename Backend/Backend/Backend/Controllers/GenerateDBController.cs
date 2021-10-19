using Backend_DAL_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateDatabaseController : ControllerBase
    {
        //readonly IConvertDbDAL _DAL;
        //public GenerateDatabaseController(IConvertDbDAL dal)
        //{
        //    _DAL = dal;
        //}

        //[HttpGet]
        //public IActionResult GenerateDatabase()
        //{
        //    _DAL.ConvertAll();

        //    return Ok();
        //}
    }
}
