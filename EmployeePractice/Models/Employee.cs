using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePractice.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required] 
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Specify the precision and scale
        public decimal Salary { get; set; }
    }
}
