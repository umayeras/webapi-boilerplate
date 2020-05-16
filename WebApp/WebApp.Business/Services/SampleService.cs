using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Business.Abstract;
using WebApp.Data.Abstract;
using WebApp.Model;

namespace WebApp.Business.Services
{
    public class SampleService : ISampleService
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
            var sample = sampleFactory.CreateAddSample(request);
            var isSuccess = sampleRepository.Add(sample);

            return !isSuccess
                ? ServiceResult.Error()
                : ServiceResult.Success();
        }

        public ServiceResult Update(UpdateSampleRequest request)
        {
            var currentSample = sampleRepository.Get(x => x.Id == request.Id);
            var sample = sampleFactory.CreateUpdateSample(request, currentSample.CreatedDate);
            var isSuccess = sampleRepository.Update(sample);

            return !isSuccess
                ? ServiceResult.Error()
                : ServiceResult.Success();
        }
    }
}