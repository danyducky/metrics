using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Startup.Configs
{
    public static class SwaggerConfig
    {
        public static void ConfigureOptions(this SwaggerGenOptions options)
        {
            var application = Assembly.GetEntryAssembly();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = application!.GetName().Name,
                Version = "v1"
            });
            
            var xmlFile = $"{application.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            
            options.IncludeXmlComments(xmlPath);
        }
    }
}