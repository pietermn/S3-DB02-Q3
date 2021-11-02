using System;
using Authentication_DTO;
using Authentication_Logic_Interface;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("SmsReciever")]
    public class SmsRecieverController : ControllerBase
    {
        readonly ISmsRecieverContainer _smsRecieverContainer;
        public SmsRecieverController(ISmsRecieverContainer smsRecieverContainer)
        {
            _smsRecieverContainer = smsRecieverContainer;
        }

        [HttpGet]
        [Route("GetAllRecievers")]
        public IActionResult GetAllRecievers()
        {
            return Ok(_smsRecieverContainer.GetRecievers());
        }

        [HttpPost]
        [Route("SetReciever")]
        public IActionResult SetReciever(SmsReciever reciever)
        {
            _smsRecieverContainer.SetReciever(reciever);
            return Ok();
        }

        [HttpPost]
        [Route("RemoveReciever")]
        public IActionResult RemoveReciever(int id)
        {
            _smsRecieverContainer.RemoveReciever(id);
            return Ok();
        }
    }
}
