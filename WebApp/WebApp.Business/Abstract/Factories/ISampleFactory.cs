using System;
using WebApp.Model;

namespace WebApp.Business.Abstract
{
    public interface ISampleFactory
    { 
        Sample CreateAddSample(AddSampleRequest request);
        Sample CreateUpdateSample(UpdateSampleRequest request, DateTime createdDate);
    }
}