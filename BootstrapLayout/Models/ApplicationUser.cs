using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BootstrapLayout.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
    }
}
