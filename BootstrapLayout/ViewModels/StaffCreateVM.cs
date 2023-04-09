using BootstrapLayout.Models;
using System.ComponentModel.DataAnnotations;

namespace BootstrapLayout.ViewModels
{
    public class StaffCreateVM
    {
        [Required]
        [Display(Name = "Staff Number")]
        public string? StaffNumber { get; set; }
        [Required]
        [Display(Name = "Staff Name")]
        public string? StaffName { get; set; }
        [Display(Name = "Staff Photo")]
        public IFormFile StaffPhoto { get; set; }
        [Required]
        [Display(Name = "Staff Email")]
        public string StaffEmail { get; set; }
        public Department Department { get; set; }
        [Required]
        public double Salary { get; set; }
    }
}
