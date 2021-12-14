using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Startup.Infrastructure
{
    public delegate void ApplicationProviderDelegate(IApplicationBuilder app, IWebHostEnvironment env);
}