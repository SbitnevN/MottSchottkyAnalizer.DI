using Microsoft.Extensions.DependencyInjection;

namespace MottSchottkyAnalizer.DI.Registration;

public class TransientAttribute<T>() : RegistrationAttribute(ServiceLifetime.Transient, typeof(T))
{
}
