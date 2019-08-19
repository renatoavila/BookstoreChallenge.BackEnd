using BookstoreChallenge.Domain.Entity;
using BookstoreChallenge.Domain.ViewModel;

namespace BookstoreChallenge.Business.Interface
{
    public interface IAuthBusiness
    {
        Users Auth(UserModel userModel);
    }
}