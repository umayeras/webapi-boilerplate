using Autofac;

namespace WebApp.Ioc.Binders
{
    public static class DataDependencyResolver
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Data.Repositories.SampleRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
