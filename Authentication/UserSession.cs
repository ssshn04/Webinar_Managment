using System.ComponentModel.DataAnnotations;

namespace WebinarManagement.Authentication
{
    public class UserSession
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set;}
        [Required(ErrorMessage = "Password is required")]
        public string Role { get; set;}
    }
}
