using BookStore.User.Context;
using BookStore.User.EmailService;
using BookStore.User.Entity;
using BookStore.User.Interface;
using BookStore.User.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;

namespace BookStore.User.Service
{
    /// <summary>
    /// User Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserDBContext dBContext;
        public readonly IConfiguration configuration;
        public UserService(UserDBContext dBContext, IConfiguration configuration)
        {
            this.dBContext = dBContext;
            this.configuration = configuration;
        }
        /// <summary>
        /// Create Jwt token
        /// </summary>
        /// <param name="userId">User Id of a user</param>
        /// <param name="email">Email Id of user</param>
        /// <returns>Jwt token</returns>
        public string JwtToken(long userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tikenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId",userId.ToString()),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tikenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// Encreption Method
        /// </summary>
        /// <param name="password">User Password</param>
        /// <returns>Encrypted Password</returns>
        public string Encrypt(string password)
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="registrationModel">Registration model</param>
        /// <returns>User Deatils</returns>
        public UserEntity User_Register(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity()
                {
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    Email = registrationModel.Email,
                    Password = Encrypt(registrationModel.Password),
                    Address = registrationModel.Address,
                };
                dBContext.Users.Add(userEntity);
                dBContext.SaveChanges();
                return userEntity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// User Log in
        /// </summary>
        /// <param name="logInModel">Login Model</param>
        /// <returns>User info with Jwt Token</returns>
        public UserLogInData UserLogIn(UserLogInModel logInModel)
        {
            try
            {
                var user = dBContext.Users.FirstOrDefault(x => x.Email == logInModel.Email && x.Password == Encrypt(logInModel.Password));
                if (user == null)
                {
                    return null;
                }
                return new UserLogInData()
                {
                    Info = user,
                    Token = JwtToken(user.UserID, user.Email)
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Forget password
        /// </summary>
        /// <param name="email">User Email Id</param>
        /// <returns>Send token to user email for </returns>
        public bool ForgetPassword(string email)
        {
            try
            {
                var user = dBContext.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    return false;
                }
                MSMQ mSMQ = new MSMQ();
                var userId = user.UserID;
                var token = JwtToken(userId, email);
                mSMQ.sendData2Queue(token);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        //RsetPassword
        public bool ResetPassword(UserResetPasswordModel resetModel, string email)
        {
            try
            {
                var user = dBContext.Users.FirstOrDefault(x => x.Email == email);
                if (user != null && resetModel.NewPassword == resetModel.ConfirmPassword)
                {
                    user.Password = Encrypt(resetModel.ConfirmPassword);
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        //Display User
        public UserEntity GetUserProfile(long userId)
        {
            try
            {
                var userInfo =  dBContext.Users.FirstOrDefault(x => x.UserID == userId);
                if (userInfo == null)
                {
                    return null;
                }
                return userInfo;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
