using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MottSchottkyAnalizer.DI.Registration;

public static class ServiceCollectionExtensions
{
    public static void Register(this IServiceCollection services, Assembly assembly)
    {
        foreach (Type type in assembly.GetTypes())
        {
            RegistrationAttribute? registration = type.GetCustomAttribute<RegistrationAttribute>(inherit: true);
            if (registration != null)
                services.Add(new ServiceDescriptor(registration.Type, type, registration.Lifetime));
        }
    }
}
