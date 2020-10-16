using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.Abstract.Services;
using WebApp.Core.Constants;
using WebApp.Model.Requests;
using WebApp.Validation.Abstract;

namespace WebApp.Controllers
{
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
            var sampleList = await sampleService.GetAll();
            return Ok(sampleList);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSampleRequest request)
        {
            var validationResult = RequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(Messages.InvalidRequest);
            }

            var result = await sampleService.Add(request);

            return Ok(result);
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateSampleRequest request)
        {
            var validationResult = RequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(Messages.InvalidRequest);
            }

            var result = await sampleService.Update(request);

            return Ok(result);
        }
        
        [HttpDelete]
        [MapToApiVersion("2")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.InvalidRequest);
            }
            
            var result = await sampleService.Delete(id);
            
            return Ok(result);
        }
    }
}