using Microsoft.Extensions.DependencyInjection;

namespace MottSchottkyAnalizer.DI.Registration;

public class SingletonAttribute<T>() : RegistrationAttribute(ServiceLifetime.Singleton, typeof(T))
{
}
