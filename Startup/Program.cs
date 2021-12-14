using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Startup.Infrastructure;

namespace Startup
{
    public static class NetCoreApp
    {
        public static void Run(IStartupConfiguration startupConfiguration)
        {
            WebHost.CreateDefaultBuilder<Startup>(Environment.GetCommandLineArgs())
                .ConfigureServices(services =>
                {
                    services.AddSingleton(startupConfiguration);
                })
                .Build()
                .Run();
        }
    }
}