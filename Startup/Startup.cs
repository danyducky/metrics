using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Startup.Configs;
using Startup.Extensions;
using Startup.Infrastructure;

namespace Startup
{
    public class Startup
    {
        private readonly IStartupConfiguration _startupConfiguration;
        private readonly Assembly _entryAssembly;

        public Startup(IConfiguration configuration, IStartupConfiguration startupConfiguration)
        {
            Configuration = configuration;
            _startupConfiguration = startupConfiguration;

            _entryAssembly = Assembly.GetEntryAssembly();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(_entryAssembly);

            services.AddApplicationDefaults(Configuration, _startupConfiguration.Collection);

            _startupConfiguration.ConfigureServices(services);

            services.AddSwaggerGen(SwaggerConfig.ConfigureOptions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseSpaStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            _startupConfiguration.ConfigureApplication(app, env);
        }
    }
}