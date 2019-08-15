using Desafio.Domain.Entity;
using Desafio.Repository.Base;
using Desafio.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Desafio.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(IConfiguration config,
                                ILogger<Repository<Client>> logger) : base(config, logger)
        {
        }


    }
}
