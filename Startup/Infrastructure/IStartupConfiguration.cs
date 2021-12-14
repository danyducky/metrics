namespace Startup.Infrastructure
{
    public interface IStartupConfiguration
    {
        DatabaseCollection Collection { get; }
        ServiceCollectionDelegate ConfigureServices { get; }
        ApplicationProviderDelegate ConfigureApplication { get; }
    }
}