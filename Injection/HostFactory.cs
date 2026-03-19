using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MottSchottkyAnalizer.DI.Registration;
using System.Reflection;

namespace MottSchottkyAnalizer.DI.Injection;

public static class HostFactory
{
    public static IHost Create(params Assembly[] assemblies)
    {
        return Host.CreateDefaultBuilder().ConfigureServices(ConfigureHost).Build();
    }

    private static void ConfigureHost(HostBuilderContext context, IServiceCollection services)
    {
        foreach (Assembly assembly in MottSchottkyAssemblies.GetAssemblies())
            services.Register(assembly);
    }

    private static class MottSchottkyAssemblies
    {
        public static Assembly[] GetAssemblies()
        {
            return
            [
                Assembly.GetExecutingAssembly(),
                Assembly.Load("MottSchottkyAnalizer.Application"),
                Assembly.Load("MottSchottkyAnalizer.Controls"),
                Assembly.Load("MottSchottkyAnalizer.Core"),
                Assembly.Load("MottSchottkyAnalizer.Infrastructure")
            ];
        }
    }
}