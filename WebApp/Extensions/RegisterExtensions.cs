using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using WebApp.Business.Factories;
using WebApp.Business.Services;
using WebApp.Core.Resources;
using WebApp.Data;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;
using WebApp.Validation;
using WebApp.Validation.Abstract;
using WebApp.Validation.RequestValidators;

namespace WebApp.Extensions
{
    internal static class RegisterExtensions
    {
        internal static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(AppSettings.DefaultConnection);
            services.AddDbContext<WebAppDbContext>(options => { options.UseSqlServer(connectionString); });
        }

        internal static void AddDependencyResolvers(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.RegisterAssemblyPublicNonGenericClasses(typeof(SampleService).Assembly)
                .Where(x => x.Name.EndsWith(ClassSuffix.Service))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            
            services.RegisterAssemblyPublicNonGenericClasses(typeof(SampleFactory).Assembly)
                .Where(x => x.Name.EndsWith(ClassSuffix.Factory))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
        }
        
        internal static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<AddSampleRequestValidator>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; 
            });
            
            services.AddSingleton<IRequestValidator, RequestValidator>();
        }
        
        internal static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebApi Boilerplate",
                    Description = "A boilerplate ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Umay ERAS",
                        Email = "umayeras@hotmail.com",
                        Url = new Uri("http://umayeras.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://github.com/umayeras/webapi-boilerplate/blob/master/LICENSE"),
                    }
                });
            });
        }
    }
}