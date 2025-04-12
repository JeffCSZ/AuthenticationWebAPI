using System.ComponentModel.DataAnnotations;

namespace AuthenticationWebAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username Not Found")]
        public string UserName { get; set; }

        [MinLength(6, ErrorMessage = "Password Length needs to be at least 6")]
        
        public string Password { get; set; }

    }
}
