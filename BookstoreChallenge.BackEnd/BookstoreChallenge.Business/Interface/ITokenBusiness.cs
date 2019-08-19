

using BookstoreChallenge.Domain.Entity;

namespace BookstoreChallenge.Business.Interface
{
    public interface ITokenBusiness
    {
        string GenerateToken(Users user);

        bool Validate(string userName, string token, out string message);

        string RefreshToken(string userName);
    }
}