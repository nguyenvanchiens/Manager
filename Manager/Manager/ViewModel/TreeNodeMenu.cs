namespace Manager.ViewModel
{
    public class TreeNodeMenu
    {
        public Guid MenuId { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public Guid ParentId { get; set; }
        public List<TreeNodeMenu> data { get; set; }
    }
}
