using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BookstoreChallenge.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerMiddlerware
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "BookstoreChallenge",
                        Version = "v1",
                        Description = "BookstoreChallenge ",
                        Contact = new Contact
                        {
                            Name = "BookstoreChallenge",
                            Url = "https://github.com/renatoavila/BookstoreChallenge.BackEnd"
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
                  
                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        public static void AddSwaggerApp(this IApplicationBuilder app, string routePrefix)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                      "BookstoreChallenger");

                c.RoutePrefix = routePrefix;
            });
        }
    }
}