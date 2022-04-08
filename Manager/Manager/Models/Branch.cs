namespace Manager.Models
{
    public class Branch
    {
        [Key]
        public Guid BrandId { get; set; }
        public string? BrandName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [ForeignKey("CountryId")]
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        [ForeignKey("LanguageId")]
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
