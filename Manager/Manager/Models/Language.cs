namespace Manager.Models
{
    public class Language
    {
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int SortOrder { get; set; }
        public List<Menu> Menu { get; set; }
        public List<Country> Countries { get; set; }
    }
}
