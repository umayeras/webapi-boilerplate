using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApp.Business.Abstract.Factories;
using WebApp.Business.Abstract.Services;
using WebApp.Core.Caching;
using WebApp.Core.Constants;
using WebApp.Core.Extensions;
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
        private readonly ICachingService cachingService;

        public SampleService(
            IUnitOfWork unitOfWork,
            ILogger<SampleService> logger,
            ISampleFactory sampleFactory,
            ICachingService cachingService) : base(unitOfWork, logger)
        {
            this.sampleFactory = sampleFactory;
            this.cachingService = cachingService;

            repository = UnitOfWork.GetRepository<Sample>();
        }

        #endregion

        public async Task<IEnumerable<Sample>> GetAll()
        {
            if (cachingService.Exists(CacheKey.Samples))
            {
                return cachingService.Get<IEnumerable<Sample>>(CacheKey.Samples);
            }

            var list = await repository.GetAllAsync(x => x.StatusId != StatusType.Deleted.ToInt32());

            AddItemsToCache(list);

            return list;
        }

        public async Task<ServiceResult> Add(AddSampleRequest request)
        {
            var sample = sampleFactory.CreateAddSample(request);

            try
            {
                await repository.AddAsync(sample);
                UnitOfWork.Save();

                AddSampleToCache(sample);
                await RefreshCachedSampleList();

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

                RefreshCachedSampleToCache(sample);
                await RefreshCachedSampleList();

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

                await RefreshCachedSampleList();

                return ServiceResult.Success(Messages.DeletingSuccess);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return ServiceResult.Error(Messages.DeletingFailed);
            }
        }

        #region caching helpers

        private void AddItemsToCache(IQueryable<Sample> samples)
        {
            cachingService.Set(CacheKey.Samples, samples.ToList());

            foreach (var item in samples)
            {
                if (!cachingService.Exists($"{CacheKey.Sample}-{item.Id}"))
                {
                    AddSampleToCache(item);
                }
            }
        }

        private void AddSampleToCache(Sample sample)
        {
            cachingService.Set($"{CacheKey.Sample}-{sample.Id}", sample);
        }

        private void RefreshCachedSampleToCache(Sample sample)
        {
            cachingService.Refresh(CacheKey.Sample, sample);
        }

        private async Task RefreshCachedSampleList()
        {
            var list = await repository.GetAllAsync(x => x.StatusId != StatusType.Deleted.ToInt32());
            cachingService.Refresh(CacheKey.Samples, list);
        }

        #endregion
    }
}