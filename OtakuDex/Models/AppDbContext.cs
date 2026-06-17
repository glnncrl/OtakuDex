using Microsoft.EntityFrameworkCore;

namespace OtakuDex.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Anime> Animes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionItem> CollectionItems { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AnimeGenre> AnimeGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AnimeGenre — composite primary key
            modelBuilder.Entity<AnimeGenre>()
                .HasKey(ag => new { ag.AnimeId, ag.GenreId });

            // AnimeGenre → Anime
            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.Anime)
                .WithMany(a => a.AnimeGenres)
                .HasForeignKey(ag => ag.AnimeId);

            // AnimeGenre → Genre
            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.AnimeGenres)
                .HasForeignKey(ag => ag.GenreId);

            // CollectionItem → Anime
            modelBuilder.Entity<CollectionItem>()
                .HasOne(ci => ci.Anime)
                .WithMany(a => a.CollectionItems)
                .HasForeignKey(ci => ci.AnimeId);

            // CollectionItem → Collection
            modelBuilder.Entity<CollectionItem>()
                .HasOne(ci => ci.Collection)
                .WithMany(c => c.CollectionItems)
                .HasForeignKey(ci => ci.CollectionId);

            // Review → Anime
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Anime)
                .WithMany(a => a.Reviews)
                .HasForeignKey(r => r.AnimeId);
        }
    }
}