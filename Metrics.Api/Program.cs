using Metrics.Api.Services;
using Metrics.DataLayer;
using Metrics.DataLayer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Startup;
using Startup.Infrastructure;

namespace Metrics.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            NetCoreApp.Run(new StartupConfiguration(new[]
            {
                (typeof(MetricsContext), typeof(IMetricsEntity))
            },
            (services) =>
            {
                // React static, only for this project
                services.AddSpaStaticFiles(configuration => { configuration.RootPath = "client-app/build"; });
                
                services.AddScoped<IUserService, UserService>()
                        .AddScoped<IMetricsService, MetricsService>();
            },
            (app, env) =>
            {
                
                // React-app, only for this project
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "client-app";

                    if (env.IsDevelopment())
                    {
                        spa.UseReactDevelopmentServer(npmScript: "start");
                    }
                });
                
            }));
        }
    }
}
