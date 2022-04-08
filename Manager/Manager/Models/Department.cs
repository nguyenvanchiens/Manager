namespace Manager.Models
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        public string DepartmentCode{ get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
