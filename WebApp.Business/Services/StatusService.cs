using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApp.Business.Abstract.Services;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;
using WebApp.Model.Entities;

namespace WebApp.Business.Services
{
    public class StatusService : BaseService<StatusService>, IStatusService
    {
        #region ctor
        
        private readonly IBaseRepository<Status> repository;

        public StatusService(
            IUnitOfWork unitOfWork,
            ILogger<StatusService> logger) : base(unitOfWork, logger)
        {
            repository = UnitOfWork.GetRepository<Status>();
        }

        #endregion

        public async Task<IEnumerable<Status>> GetAll()
        {
            return await repository.GetAllAsync();
        }
    }
}