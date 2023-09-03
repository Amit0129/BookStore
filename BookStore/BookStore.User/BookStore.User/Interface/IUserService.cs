using BookStore.User.Entity;
using BookStore.User.Model;
using System.Globalization;

namespace BookStore.User.Interface
{
    public interface IUserService
    {
        public UserEntity User_Register(UserRegistrationModel registrationModel);
        public UserLogInData UserLogIn(UserLogInModel logInModel);
        public bool ForgetPassword(string email);
        public bool ResetPassword(UserResetPasswordModel resetModel, string email);
        public UserEntity GetUserProfile(long userId);
    }
}
