using Autofac;
using FluentValidation;
using WebApp.Ioc.Extensions;
using WebApp.Model;
using WebApp.Validation;
using WebApp.Validation.Abstract;

namespace WebApp.Ioc.Binders
{
    public static class ValidationDependencyResolver
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AddSampleRequest).Assembly)
               .As(t => t.GetInterfaces())
               .Where(t => t.Name.EndsWith("Validator"))
               .InstancePerLifetimeScope();

            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
            builder.RegisterType<RequestValidator>().As<IRequestValidator>().SingleInstance();
        }
    }
}
