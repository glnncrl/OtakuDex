using System.ComponentModel.DataAnnotations;

namespace OtakuDex.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int AnimeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Your Name")]
        public string ReviewerName { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        [Display(Name = "Your Review")]
        public string Content { get; set; } = string.Empty;

        [Range(1, 10)]
        [Display(Name = "Rating (1-10)")]
        public int? Rating { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public Anime? Anime { get; set; }
    }
}