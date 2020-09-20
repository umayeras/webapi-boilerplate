using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApp.Business.Abstract.Factories;
using WebApp.Business.Abstract.Services;
using WebApp.Core.Extensions;
using WebApp.Core.Resources;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;
using WebApp.Model.Entities;
using WebApp.Model.Requests;
using WebApp.Model.Results;

namespace WebApp.Business.Services
{
    public class SampleService : BaseService<SampleService>, ISampleService
    {
        #region ctor

        private readonly ISampleFactory sampleFactory;
        private readonly IBaseRepository<Sample> repository;

        public SampleService(
            IUnitOfWork unitOfWork,
            ILogger<SampleService> logger,
            ISampleFactory sampleFactory) : base(unitOfWork, logger)
        {
            this.sampleFactory = sampleFactory;

            repository = UnitOfWork.GetRepository<Sample>();
        }

        #endregion

        public async Task<IEnumerable<Sample>> GetAll()
        {
            return await repository.GetAllAsync();
        }

        public async Task<ServiceResult> Add(AddSampleRequest request)
        {
            var sample = sampleFactory.CreateAddSample(request);

            try
            {
                await repository.AddAsync(sample);
                UnitOfWork.Save();

                return ServiceResult.Success(Messages.AddingSuccess);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return ServiceResult.Error(Messages.AddingFailed);
            }
        }

        public async Task<ServiceResult> Update(UpdateSampleRequest request)
        {
            try
            {
                var currentSample = await repository.GetAsync(x => x.Id == request.Id);
                var sample = sampleFactory.CreateUpdateSample(currentSample, request);

                repository.Update(sample);
                UnitOfWork.Save();

                return ServiceResult.Success(Messages.UpdatingSuccess);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return ServiceResult.Error(Messages.UpdatingFailed);
            }
        }

        public async Task<ServiceResult> Delete(int id)
        {
            try
            {
                var sample = await repository.GetAsync(x => x.Id == id);
                sample.StatusId = StatusType.Deleted.ToInt32();
                
                repository.Delete(sample);
                UnitOfWork.Save();

                return ServiceResult.Success(Messages.DeletingSuccess);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return ServiceResult.Error(Messages.DeletingFailed);
            }
        }
    }
}