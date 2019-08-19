using BookstoreChallenge.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreChallenge.Service.Interface
{
    public interface IUserServices
    {
        string Auth(UserModel userModel);

        bool Validate(string userName, string token, out string message);

        string RefreshToken(string userName, string token, out string message);
    }
}
