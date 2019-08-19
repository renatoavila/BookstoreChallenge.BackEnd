using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics.CodeAnalysis;

namespace BookstoreChallenge.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public static class LoggerMiddleware
    {
        public static void AddLoggerMiddleware(this IServiceCollection services)
        {
            var logger = UseLoggerMiddleware(); 
        }

        private static Logger UseLoggerMiddleware()
        {
            var logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .WriteTo.File("c:\\log\\BookstoreChallenge.Log.txt")
              .WriteTo.MongoDB("mongodb://localhost:27017/developers0012", collectionName: "BookstoreChallenge.Log")
              .CreateLogger();

            return logger;
        }
    }
}