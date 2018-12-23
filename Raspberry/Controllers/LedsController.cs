using Microsoft.AspNetCore.Mvc;
using RaspberryAPI.Controllers;
using System.Collections.Generic;

namespace Raspberry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<PinStatus>> Get()
        {
            return PinStatusMgr.Manager.Pins;
        }
        
        [HttpGet("{id}")]
        public ActionResult<PinStatus> Get(ushort number)
        {
            return PinStatusMgr.Manager.getPinByNumber(number);
        }

        //http://localhost:49932/api/leds/2/status
        [HttpPut("{id}/status")]
        public ActionResult<bool> Put(ushort number, [FromBody] PinStatusChange value)
        {
            var pin = PinStatusMgr.Manager.getPinByNumber(number);
            if(pin == null || value == null)
            {
                return false;
            }

            if (value.ledStatus)
            {
                pin.on();
            }
            else
            {
                pin.off();
            }

            return true;
        }
    }

    public class PinStatusChange
    {
        public bool ledStatus { get; set; }
    }
}
