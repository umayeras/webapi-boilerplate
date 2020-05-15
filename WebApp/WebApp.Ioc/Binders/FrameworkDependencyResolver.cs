using Autofac;
using WebApp.Framework.Abstract;
using WebApp.Framework.Services;

namespace WebApp.Ioc.Binders
{
    public static class FrameworkDependencyResolver
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LoggingService>().As<ILoggingService>().SingleInstance();
        }
    }
}
