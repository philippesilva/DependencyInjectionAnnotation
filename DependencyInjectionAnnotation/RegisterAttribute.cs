using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjectionAnnotation
{
    public sealed class RegisterAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; set; }
        public Type? InterfaceType { get; set; }

        public RegisterAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Lifetime = lifetime;
        }

        public RegisterAttribute(Type interfaceType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            InterfaceType = interfaceType;
            Lifetime = lifetime;
        }
    }
}
