using System.ComponentModel.DataAnnotations;

namespace OtakuDex.Models
{
    public class Collection
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Collection Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();
    }
}