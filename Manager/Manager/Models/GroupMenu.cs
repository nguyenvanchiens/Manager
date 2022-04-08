namespace Manager.Models
{
    public class GroupMenu
    {
        public Guid GroupMenuId { get; set; }
        public string? Title { get; set; }    
        public string? Link { get; set; }    
        public int SortOrder { get; set; }    
    }
}
