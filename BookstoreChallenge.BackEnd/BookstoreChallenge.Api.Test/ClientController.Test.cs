using BookstoreChallenge.Api.Controllers;
using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Service.Interface;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CarrosApiRest.Api.Test
{
    public class ClientController_Test
    {
        private readonly IClientServices _clientServices;

        private readonly ILogger<ClientController> _logger;

        public ClientController_Test(IClientServices clientServices,
                                ILogger<ClientController> logger)
        {
            _clientServices = clientServices;
            _logger = logger;
        }


        [Fact]
        public void TestFull()
        {
            Client client = new Client();

            client.Name = "renato";
            client.CPF = "08598003697";
            client.Email = "renato@outlook.com";

            client.Key = _clientServices.Add(client);

            client.Key = _clientServices.Add(client);

              _clientServices.Del(client.Key);

        }
    }
}
