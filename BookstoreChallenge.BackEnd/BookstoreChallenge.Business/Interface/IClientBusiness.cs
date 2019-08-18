using BookstoreChallenge.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreChallenge.Business.Interface
{
    public interface IClientBusiness
    {
        Client Get(Guid key);

        Guid Add(Client client);
         
        void Del(Guid key);

        bool CPFValidate(Client client);
    }
}
