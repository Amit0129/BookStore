using BookStore.User.Entity;
using BookStore.User.Model;

namespace BookStore.User.Interface
{
    public interface IUserService
    {
        public UserEntity User_Register(UserRegistrationModel registrationModel);
    }
}
