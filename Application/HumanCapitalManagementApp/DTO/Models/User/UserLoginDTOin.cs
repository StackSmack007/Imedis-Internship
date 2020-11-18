using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserLoginDTOin
    {

        public UserLoginDTOin(string username, string password)
        {
            UsernameOrEmail = username;
            Password = password;
        }
        public UserLoginDTOin()
        { }

        [Required, MinLength(4)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [MinLength(4), MaxLength(8), RegularExpression(@"[^\s]*(\d+|\@)[^\s]*", ErrorMessage = "Password must have atleast 1 digit!")]
        public string Password { get; set; }
    }
}