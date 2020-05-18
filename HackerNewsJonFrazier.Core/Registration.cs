using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using HackerNewsJonFrazier.Core.Services;
using HackerNewsJonFrazier.Core.Services.BusinessLogic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HackerNewsJonFrazier.Core.Tests")]
namespace HackerNewsJonFrazier.Core
{
    public class Registration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStoryService, StoryService>();

            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IUrlHelper, UrlHelper>();
        }
    }
}
