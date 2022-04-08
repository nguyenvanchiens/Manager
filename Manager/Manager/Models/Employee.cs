using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        [Required]
        public string? EmployeeName { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [ForeignKey("DepartmentId")]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
