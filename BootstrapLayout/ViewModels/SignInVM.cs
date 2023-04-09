using System.ComponentModel.DataAnnotations;

namespace BootstrapLayout.ViewModels
{
    public class SignInVM
    {
        [Required(ErrorMessage ="UserName is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
