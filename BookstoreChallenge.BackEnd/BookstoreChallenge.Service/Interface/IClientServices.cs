using BookstoreChallenge.Domain.Entity;
using System;
using System.Collections.Generic;

namespace BookstoreChallenge.Service.Interface
{
    public interface IClientServices
    {
        Client Get(Guid key);
        void Del(Guid key);
        Guid Add(Client client);
    }
}