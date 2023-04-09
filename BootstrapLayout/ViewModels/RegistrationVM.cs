using System.ComponentModel.DataAnnotations;

namespace BootstrapLayout.ViewModels
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "UserName is required")]
        [Display(Name ="User Name")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
