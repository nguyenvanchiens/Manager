using System.ComponentModel.DataAnnotations;

namespace Manager.Models
{
    public class Permission
    {
        [Key]
        public Guid PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
