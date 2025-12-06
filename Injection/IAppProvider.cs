namespace MottSchottkyAnalizer.DI.Injection;

public interface IAppProvider
{
    public T GetRequiredService<T>() where T : notnull;
}
