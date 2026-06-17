using System.ComponentModel.DataAnnotations;

namespace OtakuDex.Models
{
    public class Anime
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Genre { get; set; }

        [Range(1, 3000)]
        [Display(Name = "Episodes")]
        public int? Episodes { get; set; }

        [Display(Name = "Watch Status")]
        public string Status { get; set; } = "Plan to Watch";

        [Range(1, 10)]
        [Display(Name = "Your Rating (1-10)")]
        public int? Rating { get; set; }

        [Range(1900, 2100)]
        [Display(Name = "Year Released")]
        public int? Year { get; set; }

        [StringLength(100)]
        [Display(Name = "Studio")]
        public string? Studio { get; set; }

        [StringLength(1000)]
        [Display(Name = "Synopsis")]
        public string? Synopsis { get; set; }

        [StringLength(500)]
        [Display(Name = "Cover Image URL")]
        public string? CoverImageUrl { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();
        public ICollection<AnimeGenre> AnimeGenres { get; set; } = new List<AnimeGenre>();
    }
}