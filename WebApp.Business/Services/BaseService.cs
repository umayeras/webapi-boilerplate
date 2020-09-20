using Microsoft.Extensions.Logging;
using WebApp.Data.Uow;

namespace WebApp.Business.Services
{
    public class BaseService<TService>
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILogger<TService> Logger;

        protected BaseService(IUnitOfWork unitOfWork, ILogger<TService> logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }
    }
}