using BookstoreChallenge.Domain.Entity;
 
namespace BookstoreChallenge.Repository.Interface
{
    public interface IUserRepository : IRepository<Users>
    {
        Users GetByKey(string userName);
    }
}
