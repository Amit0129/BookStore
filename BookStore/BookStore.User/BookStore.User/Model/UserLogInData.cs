using BookStore.User.Entity;

namespace BookStore.User.Model
{
    public class UserLogInData
    {
        public UserEntity Info { get; set; }
        public string Token { get; set; }
    }
}
