using CompilerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CompilerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompilerController : ControllerBase
    {
        private readonly ICompileService _compileService;
        public CompilerController(ICommandService commandService)
        {
            OperatingSystem os = Environment.OSVersion;
            if (os.Platform == PlatformID.Win32NT)
            {
                _compileService = new WinCompileService(commandService);
            }
            else
            {
                _compileService = new LinuxCompileService(commandService);
            }
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
