using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Repository.Base;
using BookstoreChallenge.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace BookstoreChallenge.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(IConfiguration config,
                                ILogger<Repository<Users>> logger) : base(config, logger)
        {

        }

        public Users GetByKey(string userName)
        {
            var user = GetAll()?.FirstOrDefault(u => u.UserName == userName);
            return (user?.Id > 0) ? user : null;
        }
    }
}
