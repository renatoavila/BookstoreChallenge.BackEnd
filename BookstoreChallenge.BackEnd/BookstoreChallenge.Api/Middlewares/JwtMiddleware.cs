
using BookstoreChallenge.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BookstoreChallenge.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public static class JwtMiddleware
    {
        public static void AddJwtMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            UseJwtMiddleware(services, configuration);
        }

        private static void UseJwtMiddleware(IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfiguration = configuration.GetSection("TokenConfiguration").Get<TokenConfiguration>();

            services.AddSingleton<TokenConfiguration>(tokenConfiguration);
        }
    }
}