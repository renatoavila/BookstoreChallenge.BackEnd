using BookstoreChallenge.Business;
using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Repository;
using BookstoreChallenge.Repository.Interface;
using BookstoreChallenge.Service;
using BookstoreChallenge.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;


namespace BookstoreChallenge.Api.Test
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
             
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .WriteTo.File("c:\\log\\BookstoreChallenge.Log.txt")
              .WriteTo.MongoDB("mongodb://localhost:27017/developers0012", collectionName: "BookstoreChallenge.Log")
              .CreateLogger();

        }


        public void DependencyInjection(IServiceCollection services)
        { 
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddTransient<IClientBusiness, ClientBusiness>();
            services.AddTransient<IClientServices, ClientServices>();
 

        }
    }
}
