using System;
using WebApp.Business.Abstract;
using WebApp.Model;

namespace WebApp.Business.Factories
{
    public class SampleFactory : ISampleFactory
    {
        public Sample CreateAddSample(AddSampleRequest request)
        {
            return new Sample
            {
                Title = request.Title,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = 0
            };
        }
    }
}