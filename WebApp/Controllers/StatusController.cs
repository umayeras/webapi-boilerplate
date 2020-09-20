using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.Abstract.Services;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService statusService;

        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var statusList = await statusService.GetAll();
            return Ok(statusList);
        }
    }
}