using Dapper.Contrib.Extensions;
using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Repository.Base;
using BookstoreChallenge.Repository.Interface;
using BookstoreChallenge.Repository.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Data.SqlClient;
using System.Threading;
using Xunit;

namespace BookstoreChallenge.Repository.Test
{
    public class ClientRepository_Test
    {
        private readonly IConfiguration _config;
        private readonly ILogger<Repository<Client>> _logger;

        private const int RETRY = 3;
        private const int RETRY_TIME_SECONDS = 2;
        private const string DEFAULT_CONNECTION = "DefaultConnection";

        private int _retryCount = 1;
         

        [Fact]
        public void Test1()
        {
            //h9ov3cr4wcoMbLew
            //
            //"Host=35.199.105.110;Database=desafiodb;User ID=desafio;Password=h9ov3cr4wcoMbLew"
        }

        
    }
}
