using BookstoreChallenge.Business.Interface;
using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Domain.ViewModel;
using BookstoreChallenge.Repository.Interface;

namespace BookstoreChallenge.Business
{
    public sealed class AuthBusiness : IAuthBusiness 
    {
        private readonly IUserRepository _userRepository;

        public AuthBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Users Auth(UserModel userModel)
        {
            var user = GetUser(userModel.UserName);

            return !Validate(user, userModel) ? null : user;
        }

        private Users GetUser(string userName)
        {
            var user = _userRepository.GetByKey(userName);

            return user;
        }

        private static bool Validate(Users user, UserModel userModel)
        {
            if (user == null)
            {
                return false;
            }

            return user.Password == userModel.Password;
        }
    }
}