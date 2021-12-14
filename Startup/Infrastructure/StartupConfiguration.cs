using System;
using System.Collections.Generic;

namespace Startup.Infrastructure
{
    public class StartupConfiguration : IStartupConfiguration
    {
        public StartupConfiguration(IEnumerable<(Type context, Type implementation)> items, ServiceCollectionDelegate services, ApplicationProviderDelegate app)
        {
            this.Collection = new DatabaseCollection(items);
            this.ConfigureServices = services;
            this.ConfigureApplication = app;
        }
        
        public DatabaseCollection Collection { get; }
        public ServiceCollectionDelegate ConfigureServices { get; }
        public ApplicationProviderDelegate ConfigureApplication { get; }
    }
}