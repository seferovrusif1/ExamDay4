using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.AuthVMs
{
    public class LoginVM
    {
        [Required,MinLength(3)]
        public string UserNameOrEmail { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
