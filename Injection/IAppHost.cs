namespace MottSchottkyAnalizer.DI.Injection;

public interface IAppHost : IDisposable
{
    public IAppProvider AppProvider { get; }
}
