using WebApp.Data.Abstract;
using WebApp.Data.Contexts.WebApp.Data.Contexts;
using WebApp.Framework.Abstract;
using WebApp.Model;

namespace WebApp.Data.Repositories
{
    public class SampleRepository : BaseRepository<Sample, WebAppContext>, IContactRepository
    {
        public SampleRepository(ILoggingService logger) : base(logger)
        {
        }
    }
}