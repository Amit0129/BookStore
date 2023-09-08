using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using BookStore.Admin.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [HttpPost]
        [Route("/Register")]
        public IActionResult Registration(AdminRegister adminRegister)
        {
            try
            {
                var admin = adminService.Registration(adminRegister);
                if (admin != null)
                {
                    return Ok(new ResponseModel<AdminEntity> { Sucess = true, Message = "Register Sucessfull", Data = admin });
                }
                return BadRequest(new ResponseModel<string> { Sucess = false, Message = "Register Failed" });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("/Login")]
        public IActionResult AdminLogin(AdminLogin adminLogin)
        {
            try
            {
                var result = adminService.AdminLogin(adminLogin);
                if (result != null)
                {
                    return Ok(new { Sucess = true, Message = "LogIn Sucessfull", Data = result });
                }
                return BadRequest(new ResponseModel<string> { Sucess = false, Message = "LogIn Failed" });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
