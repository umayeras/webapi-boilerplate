using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Model.Entities;
using WebApp.Model.Requests;
using WebApp.Model.Results;

namespace WebApp.Business.Abstract.Services
{
    public interface ISampleService
    {
        Task<IEnumerable<Sample>> GetAll();
        Task<ServiceResult> Add(AddSampleRequest request);
        Task<ServiceResult> Update(UpdateSampleRequest request);
        Task<ServiceResult> Delete(int id);
    }
}