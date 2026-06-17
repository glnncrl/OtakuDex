namespace OtakuDex.Models
{
    public class CollectionItem
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int AnimeId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public Collection? Collection { get; set; }
        public Anime? Anime { get; set; }
    }
}