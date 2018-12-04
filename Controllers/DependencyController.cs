using Microsoft.AspNetCore.Mvc;
using Mp.Protractors.Test.Services;

namespace Mp.Protractors.Test.Controllers
{
    [Route("api/[Controller]")]
    public class DependencyController : Controller
    {
        private readonly IDependencyResolutionService _dependencyResolutionService;

        public DependencyController(IDependencyResolutionService dependencyResolutionService)
        {
            _dependencyResolutionService = dependencyResolutionService;
        }

        [HttpGet("{node}")]
        public IActionResult Index(string node)
        {
            var result = _dependencyResolutionService.ResolveDependencies(node);
            return Ok(result);
        }
    }
}