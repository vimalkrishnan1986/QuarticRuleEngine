using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Quartic.Engine.Logging;
using Quartic.Engine.Business.Common;
using Quartic.Engine.Business.Services;

namespace Quartic.Engine.Api.App_Start
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient<IRuleEngineService, RuleEngineService>();
            services.AddTransient<IRuleBusinessService, RuleBusinessService>();
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
