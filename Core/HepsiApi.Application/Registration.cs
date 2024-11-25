using FluentValidation;
using HepsiApi.Application.Bases;
using HepsiApi.Application.Behaviors;
using HepsiApi.Application.Exceptions;
using HepsiApi.Application.Features.Products.Rules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddTransient<ExceptionMiddleware>();
            services.AddRulesFromAssemblyContaining(assembly , typeof(BaseRules));

            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(assembly)
                );

            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr-TR");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
        }

        private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services , Assembly assembly , Type type)
        {

            var types = assembly.GetTypes().Where(x => x.IsSubclassOf(type) && type !=  x ).ToList();

            foreach (var item in types)
            {
                services.AddTransient(item);
            }

            return services;
        }

    }
}
