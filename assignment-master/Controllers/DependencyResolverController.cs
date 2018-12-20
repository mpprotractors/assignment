using Microsoft.AspNetCore.Mvc;
using Mp.Protractors.BLL.IServices;
using Mp.Protractors.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mp.Protractors.Web.Controllers
{
    [Route("api/[controller]")]
    public class DependencyResolverController : Controller
    {
        private readonly IDependencyResolverService _dependencyResolverService;

        public DependencyResolverController(IDependencyResolverService dependencyResolverService)
        {
            _dependencyResolverService = dependencyResolverService;
        }

        [HttpPost("resolve")]
        public IActionResult ResolveDependencies([FromBody]InputViewModel input)
        {
            var dependencies = _dependencyResolverService.ParseRawInput(input.Input);
            _dependencyResolverService.ResolveDependencies(dependencies);
            return Ok(dependencies.Select(x => x.ToString()));
        }
    }
}
