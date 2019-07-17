using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gzh.Template.Core.ApiGateway {
    public class Program {
        // public static void Main (string[] args) {
        //     new WebHostBuilder ()
        //         .UseKestrel ()
        //         .UseContentRoot (Directory.GetCurrentDirectory ())
        //         .ConfigureAppConfiguration ((hostingContext, config) => {
        //             config
        //                 .SetBasePath (hostingContext.HostingEnvironment.ContentRootPath)
        //                 .AddJsonFile ("appsettings.json", true, true)
        //                 .AddJsonFile ($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
        //                 .AddJsonFile ("ocelot.json")
        //                 .AddEnvironmentVariables ();
        //         })
        //         .ConfigureServices (services => {
        //             //services.AddOcelot ().AddConsul ();
        //             services.AddOcelot ();
        //         })
        //         .ConfigureLogging ((hostingContext, logging) => {
        //             //add your logging
        //         })
        //         .UseIISIntegration ()
        //         .Configure (app => {
        //             app.UseOcelot ().Wait ();
        //         })
        //         .Build ()
        //         .Run ();
        // }

        public static void Main (string[] args) {
            CreateWebHostBuilder (args).Build ().Run ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .ConfigureAppConfiguration ((hostingContext, builder) => {
                builder
                    .SetBasePath (hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile ("Ocelot.json");
            })
            .UseStartup<Startup> ();
    }
}