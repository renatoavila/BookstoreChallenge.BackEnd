using BookstoreChallenge.Business;
using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Repository;
using BookstoreChallenge.Repository.Interface;
using BookstoreChallenge.Service;
using BookstoreChallenge.Service.Interface;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;

namespace BookstoreChallenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjection(services);
            Configuration.GetSection("DefaultConnection");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpClient();
             
            // Configurando o serviço de documentação do Swagger
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

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .WriteTo.File("c:\\log\\BookstoreChallenge.Log.txt")
              .WriteTo.MongoDB("mongodb://localhost:27017/developers0012", collectionName: "BookstoreChallenge.Log")
              .CreateLogger();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "BookstoreChallenger");
            });
        }


        public void DependencyInjection(IServiceCollection services)
        { 
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddTransient<IClientBusiness, ClientBusiness>();
            services.AddTransient<IClientServices, ClientServices>();
 

        }
    }
}
