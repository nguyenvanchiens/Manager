namespace Manager.Models
{
    public class Country
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Description { get; set; }
        [ForeignKey("LanguageId")]
        public Guid LanguageId { get; set; }
        public Language? Language { get; set; }
        public string? CreatedBy { get; set; }  
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
