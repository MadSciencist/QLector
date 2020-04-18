using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QLector.DAL.EF;
using Serilog;
using System;

namespace QLector.Api
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = CreateSerilogLogger();

            try
            {
                Log.Information("Starting web host...");
                var webHost = CreateHostBuilder(args).Build();
                InitializeDatabase(webHost);
                webHost.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Fatal error occured");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void InitializeDatabase(IHost webHost)
        {
            var initializer = webHost.Services.GetRequiredService<IDbInitializer>();
            initializer.Initialize().Wait();
            initializer.Seed().Wait();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static ILogger CreateSerilogLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                //.WriteTo.File(new CompactJsonFormatter(), "qlectorApi.log")
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();
        }
    }
}
