using Microsoft.Extensions.DependencyInjection;

namespace MottSchottkyAnalizer.DI.Registration;

public class ScopedAttribute<T>() : RegistrationAttribute(ServiceLifetime.Scoped, typeof(T))
{
}
