using Microsoft.Extensions.DependencyInjection;
using System;
using Quartic.Engine.Logging;
using Quartic.Engine.Api.App_Start;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore;
using Quartic.Engine.Helper;
namespace Quartic.Engine.Host
{
    class Program
    {
        const string _hostUrlKey = "HostUrl";
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {

                })
                .UseContentRoot(pathToContentRoot)
                .UseUrls(ConfigHelper.GetAppSettingValue<string>(_hostUrlKey))
                                .UseStartup<Startup>();
        }
    }
}
