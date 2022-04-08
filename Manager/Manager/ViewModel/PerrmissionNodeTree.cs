namespace Manager.ViewModel
{
    public class PerrmissionNodeTree
    {
        public Guid PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<PerrmissionNodeTree> Childrent { get; set; }

    }
}
