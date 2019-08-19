
using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Domain.ViewModel;
using BookstoreChallenge.Service.Interface;

namespace BookstoreChallenge.Service
{
    public sealed class UserServices : IUserServices
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly ITokenBusiness _tokenBusiness;

        public UserServices(IAuthBusiness authBusiness, ITokenBusiness tokenBusiness)
        {
            _authBusiness = authBusiness;
            _tokenBusiness = tokenBusiness;
        }

        public string Auth(UserModel userModel)
        {
            var user = _authBusiness.Auth(userModel);

            if (user == null)
            {
                return null;
            }

            var token = _tokenBusiness.GenerateToken(user);

            return token;
        }

        public bool Validate(string userName, string token, out string message)
        {
            var result = _tokenBusiness.Validate(userName, token, out message);

            return result;
        }

        public string RefreshToken(string userName, string token, out string message)
        {
            var result = Validate(userName, token, out message);

            if (!result)
            {
                return null;
            }

            var refreshedToken = _tokenBusiness.RefreshToken(userName);

            return refreshedToken;
        }
    }
}