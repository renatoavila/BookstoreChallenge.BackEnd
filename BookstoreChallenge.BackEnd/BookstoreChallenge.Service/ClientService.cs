using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Service.Interface;
using System;
using System.Diagnostics.Contracts;

namespace BookstoreChallenge.Service
{
    public class ClientServices : IClientServices
    {
        private readonly IClientBusiness _clientBusiness;


        public ClientServices(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        public Guid Add(Client client)
        {
            client.AddNotification(!_clientBusiness.CPFValidate(client), "CPF inválido.");

            if (client.Valid)
            {
                _clientBusiness.Add(client);
            }

            return client.Key;
        }

        public Client Get(Guid key)
        {
            return _clientBusiness.Get(key);
        }

        public void Del(Guid key)
        {
            _clientBusiness.Del(key);
        }
    }
}
