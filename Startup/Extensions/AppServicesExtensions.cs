using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Startup.Infrastructure;

namespace Startup.Extensions
{
    public static class AppServicesExtensions
    {
        internal static void AddApplicationDefaults(this IServiceCollection services, IConfiguration configuration,
            DatabaseCollection collection)
        {
            foreach ((Type context, Type implementation) item in collection)
            {
                var connectionString = configuration.GetConnectionString(item.context.Name);
                // Adding db context providers
                typeof(EntityFrameworkServiceCollectionExtensions)
                    .GetMethod("AddDbContext", 1,
                        new Type[]
                        {
                            typeof(IServiceCollection), typeof(Action<DbContextOptionsBuilder>),
                            typeof(ServiceLifetime), typeof(ServiceLifetime)
                        })!
                    .MakeGenericMethod(item.context)
                    .Invoke(services,
                        new object[]
                        {
                            services,
                            new Action<DbContextOptionsBuilder>(options => options.UseNpgsql(connectionString)),
                            ServiceLifetime.Scoped, null
                        });

                // Scoped services
                services.Scan(scan => scan.FromAssemblies(item.context.Assembly)
                    .AddClasses(x => x.Where(type => !type.IsAbstract && !type.IsSealed && !type.IsNested
                                                     && type.Name.EndsWith("Repository")))
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

                // Singleton services
                services.Scan(scan => scan.FromAssemblies(item.context.Assembly)
                    .AddClasses(x => x.Where(type => !type.IsAbstract && !type.IsSealed && !type.IsNested
                                                     && type.Name.EndsWith("Factory")))
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithSingletonLifetime());
            }
        }
    }
}