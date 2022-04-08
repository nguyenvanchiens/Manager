namespace Manager.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        [ForeignKey("PermissionId")]
        public Guid? PermissionId { get; set; }
        public Permission? Permission { get; set; }
        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool Licensed { get; set; }
    }
}
