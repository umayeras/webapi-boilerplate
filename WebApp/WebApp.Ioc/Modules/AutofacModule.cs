using Autofac;
using WebApp.Ioc.Binders;

namespace WebApp.Ioc.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            BusinessDependencyResolver.RegisterServices(builder);
            FrameworkDependencyResolver.RegisterServices(builder);
            DataDependencyResolver.RegisterServices(builder);
            ValidationDependencyResolver.RegisterServices(builder);
        }
    }
}
