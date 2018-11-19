using Microsoft.AspNetCore.Mvc;
using Mp.Protractors.Test.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Mp.Protractors.Test.Controllers
{
    [Route("api/solve")]
    public class SolveController : ControllerBase
    {
        private ISolver _solver;

        public SolveController (ISolver solver)
        {
            _solver = solver;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<FactDTO> facts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _solver.Solve(facts);

            if (result.Success) 
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}