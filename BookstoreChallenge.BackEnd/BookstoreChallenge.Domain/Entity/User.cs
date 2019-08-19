
namespace BookstoreChallenge.Domain.Entity
{
    public sealed class Users :  Base.Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
        //public override string SearchKey => UserName;
    }
}