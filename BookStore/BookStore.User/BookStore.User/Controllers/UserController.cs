using BookStore.User.Entity;
using BookStore.User.Interface;
using BookStore.User.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        //User Register Api
        [HttpPost]
        public IActionResult User_Register(UserRegistrationModel registrationModel)
        {
            try
            {
                var user = userService.User_Register(registrationModel);
                if (user != null)
                {
                    return Ok(new { sucess = true, message = "Register Sucessfull", data = user });
                }
                return BadRequest(new { sucess = false, message = "Register Failed" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
