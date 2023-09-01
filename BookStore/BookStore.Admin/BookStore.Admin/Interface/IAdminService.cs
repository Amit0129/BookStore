using BookStore.Admin.Entity;
using BookStore.Admin.Model;

namespace BookStore.Admin.Interface
{
    public interface IAdminService
    {
        public AdminEntity Registration(AdminRegister adminRegister);
        public AdminLoginDeatils AdminLogin(AdminLogin adminLogin);
        public string JWTTokenGenerator(long adminId, string email);
    }
}
