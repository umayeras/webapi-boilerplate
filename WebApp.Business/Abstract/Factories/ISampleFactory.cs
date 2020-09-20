using System;
using WebApp.Model.Entities;
using WebApp.Model.Requests;

namespace WebApp.Business.Abstract.Factories
{
    public interface ISampleFactory
    {
        Sample CreateAddSample(AddSampleRequest request);
        Sample CreateUpdateSample(Sample sample, UpdateSampleRequest request);
    }
}