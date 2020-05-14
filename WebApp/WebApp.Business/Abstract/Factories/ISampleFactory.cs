using WebApp.Model;

namespace WebApp.Business.Abstract
{
    public interface ISampleFactory
    { 
        Sample CreateAddSample(AddSampleRequest request);
    }
}