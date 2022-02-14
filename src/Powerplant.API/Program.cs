using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Powerplant.API.Logs.Configurations;
using Powerplant.Infra.Data.InitializeData;
using Serilog;
using System;

namespace Powerplant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = SerilogConfig.CreateLogger();

            try
            {
                Log.Information("Starting up");

                var host = CreateHostBuilder(args).Build();

                var dataBaseProviderhost = host.InitDataBaseAsync().Result;

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseSerilog()
                       .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}
