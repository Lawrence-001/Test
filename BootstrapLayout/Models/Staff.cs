using System.ComponentModel.DataAnnotations;

namespace BootstrapLayout.Models
{
    public class Staff
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Staff Number")]
        public string? StaffNumber { get; set; }

        [Required]
        [Display(Name = "Staff Name")]
        public string? StaffName { get; set; }

        [Display(Name = "Staff Photo")]
        public string? StaffPhoto { get; set; }

        [Required]
        [Display(Name = "Staff Email")]
        [DataType(DataType.EmailAddress)]
        public string? StaffEmail { get; set; }

        public Department? Department { get; set; }

        [Required]
        public double Salary { get; set; }
    }
}
