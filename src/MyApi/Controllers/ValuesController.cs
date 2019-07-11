using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyApi.Config;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MyConfig _config;

        public ValuesController(IOptions<MyConfig> options)
        {
            _config = options.Value;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            if (_config.IsEnabled)
                return Ok(_config.Text);

            return NotFound();
        }
    }
}
