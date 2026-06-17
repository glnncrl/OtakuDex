using System.ComponentModel.DataAnnotations.Schema;

namespace OtakuDex.Models
{
    [Table("AnimeDatabase")]
    public class AnimeDatabase
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Studio { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Episodes { get; set; }
        public double MalScore { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
    }
}