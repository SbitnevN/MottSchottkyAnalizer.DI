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
        foreach (Assembly assembly in MottSchottkyAssemblies.All)
            services.Register(assembly);
    }

    private static class MottSchottkyAssemblies
    {
        public static readonly Assembly Application = Assembly.Load("MottSchottkyAnalizer.Application");
        public static readonly Assembly Controls = Assembly.Load("MottSchottkyAnalizer.Controls");
        public static readonly Assembly Core = Assembly.Load("MottSchottkyAnalizer.Core");
        public static readonly Assembly Infrastructure = Assembly.Load("MottSchottkyAnalizer.Infrastructure");
        public static Assembly DI => Assembly.GetExecutingAssembly();

        public static Assembly[] All { get; } = [DI, Application, Controls, Core, Infrastructure];
    }
}