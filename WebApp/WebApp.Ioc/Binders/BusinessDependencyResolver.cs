using Autofac;

namespace WebApp.Ioc.Binders
{
    public static class BusinessDependencyResolver
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Business.Services.SampleService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(Business.Factories.SampleFactory).Assembly)
                .Where(t => t.Name.EndsWith("Factory"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}