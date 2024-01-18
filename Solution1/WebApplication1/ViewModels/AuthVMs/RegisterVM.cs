using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.AuthVMs
{
    public class RegisterVM
    {
        [Required,MinLength(3)]
        public string FullName { get; set; }
        [Required, MinLength(3)]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
