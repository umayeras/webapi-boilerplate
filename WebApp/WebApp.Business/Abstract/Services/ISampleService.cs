using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.Business.Abstract
{
    public interface ISampleService
    {
        Task<IEnumerable<Sample>> Get();
        
        ServiceResult Add(AddSampleRequest request);

        ServiceResult Update(UpdateSampleRequest request);
    }
}