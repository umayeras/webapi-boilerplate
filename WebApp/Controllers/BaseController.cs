using Microsoft.AspNetCore.Mvc;
using WebApp.Validation.Abstract;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1")]
    [ApiVersion("2")]
    public class BaseController : ControllerBase
    {
        protected readonly IRequestValidator RequestValidator;

        public BaseController(IRequestValidator requestValidator)
        {
            RequestValidator = requestValidator;
        }
    }
}