using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.User.Model
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "FirstName {0} is required")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "FirstName's length must greater than 3 character,Maximum length is 50")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName {0} is required")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "LastName's length must greater than 3 character,Maximum length is 50")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email {0} is required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password {0} is required")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }


        public string Address { get; set; }
    }
}
