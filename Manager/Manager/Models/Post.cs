namespace Manager.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        public int Language { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public bool isComment { get; set; } = true;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? LimitedDate { get; set; }
        public string? SeoTitle { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public string? LinkImage { get; set; }
        public bool isShowHome { get; set; } = true;
        public DateTime? CreatedDate { get; set; }

    }
}
