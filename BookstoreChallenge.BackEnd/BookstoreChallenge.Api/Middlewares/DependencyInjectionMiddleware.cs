using BookstoreChallenge.Business;
using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Repository;
using BookstoreChallenge.Repository.Interface;
using BookstoreChallenge.Service;
using BookstoreChallenge.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BookstoreChallenge.Api.Middlewares
{
    /// <summary>
    /// Classe de inserção de injeção de dependência
    /// </summary>
    public static class DependencyInjectionMiddleware
    {
        /// <summary>
        /// Método de inserção de injeção de dependência
        /// </summary>
        /// <param name="services">serviços</param>
        public static void AddDependencyInjection(this IServiceCollection services)
        {

            services.AddSingleton<IUserRepository, UserRepository>(); 
            services.AddTransient<IUserServices, UserServices>();
             
            services.AddTransient<IAuthBusiness, AuthBusiness>();
            services.AddTransient<ITokenBusiness, TokenBusiness>();
            services.AddTransient<IUserServices, UserServices>();

            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddTransient<IClientBusiness, ClientBusiness>();
            services.AddTransient<IClientServices, ClientServices>();
        }
    }
}