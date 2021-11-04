using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace telnyxApi.Controllers
{
    [ApiController]
    [Route("Telnyx")]
    public class TelnyxAPIController : ControllerBase
    {
        TelnyxAPI api = new();
        private readonly ILogger<TelnyxAPIController> _logger;

        [Route("create"), HttpPost]
        public async Task<IActionResult> SendAsync(Sms sms)
        {
            

            if (await api.SendMessage(sms))
            {
                return Ok();
            }
            else
            {
                //Error in code
                return StatusCode(501);
            }
        }
    }
}
