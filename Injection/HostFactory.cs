using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MottSchottkyAnalizer.DI.Registration;
using System.Reflection;

namespace MottSchottkyAnalizer.DI.Injection;

public static class HostFactory
{
    public static IAppHost Create(params Assembly[] assemblies)
    {
        IHost host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            foreach (Assembly assembly in LoadAssemblies())
                services.Register(assembly);

            services.AddSingleton<IAppProvider, AppProvider>();
        }).Build();

        return new AppHost(host);
    }
    private static Assembly[] LoadAssemblies()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory;
        List<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        var dllFiles = Directory.GetFiles(path, "*.dll").Where(dll => Path.GetFileName(dll).StartsWith("MottSchottkyAnalizer")).ToArray();

        foreach (string dll in dllFiles)
        {
            if (loadedAssemblies.Any(a =>
                !string.IsNullOrEmpty(a.Location) &&
                string.Equals(a.Location, dll, StringComparison.OrdinalIgnoreCase)))
            {
                continue;
            }

            try
            {
                var assembly = Assembly.LoadFrom(dll);
                loadedAssemblies.Add(assembly);
            }
            catch (BadImageFormatException)
            {
                // Игнорируем не .NET DLL
            }
            catch (FileLoadException)
            {
                // Иногда сборка уже загружена другим способом, можно безопасно игнорировать
            }
        }

        return loadedAssemblies.ToArray();
    }

    private class AppHost : IAppHost
    {
        private readonly IHost _host;

        public IAppProvider AppProvider { get; }

        public AppHost(IHost host)
        {
            _host = host;
            AppProvider = new AppProvider(_host.Services);
        }

        public void Dispose()
        {
            _host.StopAsync().Wait();
            _host.Dispose();
        }

        public T GetRequiredService<T>() where T : notnull
        {
            return _host.Services.GetRequiredService<T>();
        }
    }

    [Service<IAppProvider>]
    private class AppProvider : IAppProvider
    {
        private readonly IServiceProvider _serviceProvider;
        public AppProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetRequiredService<T>() where T : notnull
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
