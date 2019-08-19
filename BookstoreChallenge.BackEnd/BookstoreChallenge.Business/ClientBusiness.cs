using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreChallenge.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;

        public ClientBusiness(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Get(Guid key)
        {
            return _clientRepository.Get(key);
        }

        public Guid Add(Client client)
        {
            if (!_clientRepository.Update(client))
                _clientRepository.Insert(client);

            return client.Key;
        }

        public void Del(Guid key)
        {
            _clientRepository.Delete(new Client { Key = key });
        }

        public bool CPFValidate(Client client)
        {
            //todo: implementar validador de CPF
            return true;
        }

    }
}
