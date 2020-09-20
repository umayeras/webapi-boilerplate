using System;
using WebApp.Business.Abstract.Factories;
using WebApp.Model.Entities;
using WebApp.Model.Requests;

namespace WebApp.Business.Factories
{
    public class SampleFactory : ISampleFactory
    {
        public Sample CreateAddSample(AddSampleRequest request)
        {
            return new Sample
            {
                Title = request.Title,
                CreatedBy = "user-1"
            };
        }

        public Sample CreateUpdateSample(Sample sample, UpdateSampleRequest request)
        {
            sample.Title = request.Title;
            sample.StatusId = request.Status;
            sample.ModifiedBy = "user-2";
            sample.ModifiedDate = DateTime.Now.ToLocalTime();

            return sample;
        }
    }
}