﻿using BookStore.Admin.Context;
using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using BookStore.Admin.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Admin.Service
{
    public class AdminService : IAdminService
    {
        /// <summary>
        /// Admin Service Layer 
        /// </summary>
        private readonly AdminContext adminContext;
        public readonly IConfiguration configuration;
        public AdminService(AdminContext adminContext, IConfiguration configuration)
        {
            this.adminContext = adminContext;
            this.configuration = configuration;
        }
        /// <summary>
        /// New Admin register
        /// </summary>
        /// <param name="adminRegister">Registration Model</param>
        /// <returns>Admin Entity Model</returns>
        public AdminEntity Registration(AdminRegister adminRegister)
        {
            try
            {
                AdminEntity adminEntity = new AdminEntity()
                {
                    FirstName = adminRegister.FirstName,
                    LastName = adminRegister.LastName,
                    Email = adminRegister.Email,
                    Password = Encrypt(adminRegister.Password)
                };
                adminContext.Admin.Add(adminEntity);
                adminContext.SaveChanges();
                return adminEntity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="adminLogin">Login Model</param>
        /// <returns>Admin Info and JWT token</returns>
        /// <exception cref="Exception"></exception>
        public AdminLoginDeatils AdminLogin(AdminLogin adminLogin)
        {
            try
            {
                var enPassword = Encrypt(adminLogin.Password);
                var login = adminContext.Admin.FirstOrDefault(x => x.Email == adminLogin.Email && x.Password == enPassword);

                if (login == null)
                {
                    return null;
                }
                else
                {
                    return new AdminLoginDeatils()
                    {
                        Data = login,
                        Token = JWTTokenGenerator(login.AdminID, login.Email)
                    };
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Create Jwt Token
        /// </summary>
        /// <param name="adminId">An exiting admin ID</param>
        /// <param name="email">Email of the admin</param>
        /// <returns>Jwt Token</returns>
        public string JWTTokenGenerator(long adminId, string email)
        {
            var tokenHanler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("AdminId", adminId.ToString()),
                    new Claim(ClaimTypes.Role,"Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(25),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHanler.CreateToken(tokenDescriptor);
            return tokenHanler.WriteToken(token);
        }
        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <param name="password">Admin Password</param>
        /// <returns>Encreypted password</returns>
        public string Encrypt(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return "";
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
    }
}
