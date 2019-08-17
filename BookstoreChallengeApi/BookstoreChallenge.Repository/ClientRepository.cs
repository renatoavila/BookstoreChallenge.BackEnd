using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Repository.Base;
using BookstoreChallenge.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace BookstoreChallenge.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(IConfiguration config,
                                ILogger<Repository<Client>> logger) : base(config, logger)
        {
        }


    }
}
