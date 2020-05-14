using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Business.Abstract;
using WebApp.Data.Abstract;
using WebApp.Model;

namespace WebApp.Business.Services
{
    public class SampleService :ISampleService
    {
        private readonly ISampleRepository sampleRepository;
        private readonly ISampleFactory sampleFactory;
        
        public SampleService(
            ISampleRepository sampleRepository, 
            ISampleFactory sampleFactory)
        {
            this.sampleRepository = sampleRepository;
            this.sampleFactory = sampleFactory;
        }
        
        public async Task<IEnumerable<Sample>> Get()
        {
            return await sampleRepository.GetListAsync();
        }

        public ServiceResult Add(AddSampleRequest request)
        {
            var contact = sampleFactory.CreateAddSample(request);
            var isSuccess = sampleRepository.Add(contact);
            
            return !isSuccess 
                ? ServiceResult.Error() 
                : ServiceResult.Success();
        }
    }
}