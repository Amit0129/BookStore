using BookStore.User.Context;
using BookStore.User.Entity;
using BookStore.User.Interface;
using BookStore.User.Model;
using System.Text;

namespace BookStore.User.Service
{
    public class UserService : IUserService
    {
        private readonly UserDBContext dBContext;
        public UserService(UserDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        //User Registration
        public UserEntity User_Register(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity()
                {
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    Email = registrationModel.Email,
                    Password= Encrypt(registrationModel.Password),
                    Address= registrationModel.Address,
                };
                dBContext.Users.Add(userEntity);
                dBContext.SaveChanges();
                return userEntity;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Encription 
        public string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                var passwordByte = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordByte);
            }

        }
    }
}
