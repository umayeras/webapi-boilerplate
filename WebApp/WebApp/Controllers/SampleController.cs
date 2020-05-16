using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.Abstract;
using WebApp.Model;
using WebApp.Validation.Abstract;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : BaseController
    {
        private readonly ISampleService sampleService;

        public SampleController(
            IRequestValidator requestValidator,
            ISampleService sampleService) : base(requestValidator)
        {
            this.sampleService = sampleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sampleList = await sampleService.Get();
            return Ok(sampleList);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] AddSampleRequest request)
        {
            var validationResult = RequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest("InvalidRequest");
            }

            var result = sampleService.Add(request);

            return Ok(result);
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] UpdateSampleRequest request)
        {
            var validationResult = RequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest("InvalidRequest");
            }

            var result = sampleService.Update(request);

            return Ok(result);
        }
    }
}