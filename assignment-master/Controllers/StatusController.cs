using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Mp.Protractors.Test.Controllers
{
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() {
            return Ok("I'm ok");
        }
    }
}