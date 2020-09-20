using Microsoft.AspNetCore.Mvc;
using WebApp.Validation.Abstract;

namespace WebApp.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IRequestValidator RequestValidator;

        public BaseController(IRequestValidator requestValidator)
        {
            RequestValidator = requestValidator;
        }
    }
}