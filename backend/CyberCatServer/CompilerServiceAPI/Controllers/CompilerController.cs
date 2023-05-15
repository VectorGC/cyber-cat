using CompilerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompilerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompilerController : ControllerBase
    {
        private readonly ICompileService _compileService;
        public CompilerController(ICompileService compileService)
        {
            _compileService = compileService;
        }

        [HttpPost]
        public IActionResult CompileCode(string code)
        {
            var res = string.Empty;
            res = _compileService.CompileCode(code);
            if (!string.IsNullOrEmpty(res))
                return Ok(res);
            else
            {
                res = _compileService.LaunchCode();
                return Ok(res);
            }
        }
    }
}
