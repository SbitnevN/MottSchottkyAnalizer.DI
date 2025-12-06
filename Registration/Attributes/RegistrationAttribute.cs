using Microsoft.Extensions.DependencyInjection;

namespace MottSchottkyAnalizer.DI.Registration;

[AttributeUsage(AttributeTargets.Class)]
public class RegistrationAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; init; }

    public Type Type { get; init; }

    public RegistrationAttribute(ServiceLifetime serviceLifetime, Type type)
    {
        Lifetime = serviceLifetime;
        Type = type;
    }
}
