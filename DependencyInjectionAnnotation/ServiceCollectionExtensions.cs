using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DependencyInjectionAnnotation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScanAndRegister(this IServiceCollection services, params string[] fullAssemblyNames)
        {
            var types = fullAssemblyNames.SelectMany(fullAssemblyName =>
            {
                var assembly = Assembly.Load(fullAssemblyName);

                var types = assembly.GetTypes();

                return types;

            });
            
            foreach (var implementationType in types)
            {
                var injectorAttribute = implementationType.GetCustomAttribute<RegisterAttribute>();

                if (injectorAttribute == null)
                {
                    continue;
                }

                switch (injectorAttribute.Lifetime)
                {
                    case ServiceLifetime.Scoped:
                        services.RegisterScoped(injectorAttribute.InterfaceType, implementationType);
                        break;
                    case ServiceLifetime.Singleton:
                        services.RegisterSingleton(injectorAttribute.InterfaceType, implementationType);
                        break;
                    case ServiceLifetime.Transient:
                        services.RegisterTransient(injectorAttribute.InterfaceType, implementationType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return services;
        }

        private static void RegisterScoped(this IServiceCollection services, Type? serviceType, Type implementationType)
        {
            if (serviceType == null)
            {
                services.AddScoped(implementationType);
            }
            else
            {
                services.AddScoped(serviceType, implementationType);
            }
        }

        private static void RegisterSingleton(this IServiceCollection services, Type? serviceType, Type implementationType)
        {
            if (serviceType == null)
            {
                services.AddSingleton(implementationType);
            }
            else
            {
                services.AddSingleton(serviceType, implementationType);
            }
        }

        private static void RegisterTransient(this IServiceCollection services, Type? serviceType, Type implementationType)
        {
            if (serviceType == null)
            {
                services.AddTransient(implementationType);
            }
            else
            {
                services.AddTransient(serviceType, implementationType);
            }
        }
    }
}
