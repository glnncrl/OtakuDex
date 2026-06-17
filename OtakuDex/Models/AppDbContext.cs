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
        public DbSet<AnimeDatabase> AnimeDatabase { get; set; }

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

            modelBuilder.Entity<AnimeDatabase>().HasData(
                new AnimeDatabase { Id = 1, Title = "Steins;Gate", Genre = "Sci-Fi,Thriller,Drama", Studio = "White Fox", Year = 2011, Episodes = 24, MalScore = 9.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/5/73199l.jpg", Synopsis = "A self-proclaimed mad scientist discovers time travel and must face its consequences." },
                new AnimeDatabase { Id = 2, Title = "Hunter x Hunter (2011)", Genre = "Action,Adventure,Fantasy", Studio = "Madhouse", Year = 2011, Episodes = 148, MalScore = 9.0, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1337/99013l.jpg", Synopsis = "A young boy searches for his missing father in a world of hunters." },
                new AnimeDatabase { Id = 3, Title = "Neon Genesis Evangelion", Genre = "Action,Sci-Fi,Drama,Psychological", Studio = "Gainax", Year = 1995, Episodes = 26, MalScore = 8.5, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1314/108888l.jpg", Synopsis = "Teenagers pilot giant mechs to protect humanity from mysterious beings." },
                new AnimeDatabase { Id = 4, Title = "Cowboy Bebop", Genre = "Action,Sci-Fi,Drama", Studio = "Sunrise", Year = 1998, Episodes = 26, MalScore = 8.8, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/4/19644l.jpg", Synopsis = "A ragtag crew of bounty hunters travel the solar system." },
                new AnimeDatabase { Id = 5, Title = "One Piece", Genre = "Action,Adventure,Comedy,Fantasy", Studio = "Toei Animation", Year = 1999, Episodes = 1000, MalScore = 8.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/6/73245l.jpg", Synopsis = "A young pirate sets out to find the ultimate treasure and become King of the Pirates." },
                new AnimeDatabase { Id = 6, Title = "Naruto Shippuden", Genre = "Action,Adventure,Fantasy", Studio = "Pierrot", Year = 2007, Episodes = 500, MalScore = 8.2, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1565/111305l.jpg", Synopsis = "Naruto continues his journey to become Hokage and save his friend Sasuke." },
                new AnimeDatabase { Id = 7, Title = "Death Note", Genre = "Thriller,Supernatural,Drama,Psychological", Studio = "Madhouse", Year = 2006, Episodes = 37, MalScore = 8.6, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/9/9453l.jpg", Synopsis = "A genius student finds a notebook that can kill anyone whose name is written in it." },
                new AnimeDatabase { Id = 8, Title = "Code Geass", Genre = "Action,Sci-Fi,Drama,Mecha", Studio = "Sunrise", Year = 2006, Episodes = 25, MalScore = 8.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/5/50331l.jpg", Synopsis = "An exiled prince gains the power to command anyone and leads a rebellion." },
                new AnimeDatabase { Id = 9, Title = "Made in Abyss", Genre = "Adventure,Drama,Fantasy,Sci-Fi", Studio = "Kinema Citrus", Year = 2017, Episodes = 13, MalScore = 8.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/6/86733l.jpg", Synopsis = "A girl and a robot boy descend into a mysterious and dangerous abyss." },
                new AnimeDatabase { Id = 10, Title = "Re:Zero", Genre = "Drama,Fantasy,Psychological,Thriller", Studio = "White Fox", Year = 2016, Episodes = 25, MalScore = 8.3, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1522/128039l.jpg", Synopsis = "A boy is transported to a fantasy world and discovers he revives upon death." },
                new AnimeDatabase { Id = 11, Title = "Sword Art Online", Genre = "Action,Adventure,Fantasy,Romance", Studio = "A-1 Pictures", Year = 2012, Episodes = 25, MalScore = 7.2, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/11/39717l.jpg", Synopsis = "Players are trapped in a virtual reality MMORPG and must clear it to escape." },
                new AnimeDatabase { Id = 12, Title = "No Game No Life", Genre = "Comedy,Fantasy,Ecchi", Studio = "Madhouse", Year = 2014, Episodes = 12, MalScore = 8.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1074/111944l.jpg", Synopsis = "Genius gamer siblings are transported to a world where everything is decided by games." },
                new AnimeDatabase { Id = 13, Title = "Overlord", Genre = "Action,Adventure,Fantasy", Studio = "Madhouse", Year = 2015, Episodes = 13, MalScore = 7.9, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/7/88211l.jpg", Synopsis = "A player is stuck in an RPG as his character and builds his own empire." },
                new AnimeDatabase { Id = 14, Title = "That Time I Got Reincarnated as a Slime", Genre = "Action,Adventure,Comedy,Fantasy", Studio = "8bit", Year = 2018, Episodes = 24, MalScore = 8.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1506/117736l.jpg", Synopsis = "A man reincarnates as a slime in a fantasy world and builds his own nation." },
                new AnimeDatabase { Id = 15, Title = "Black Clover", Genre = "Action,Comedy,Fantasy", Studio = "Pierrot", Year = 2017, Episodes = 170, MalScore = 8.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/2/88336l.jpg", Synopsis = "A boy born without magic powers dreams of becoming the Wizard King." },
                new AnimeDatabase { Id = 16, Title = "Bleach", Genre = "Action,Supernatural,Adventure", Studio = "Pierrot", Year = 2004, Episodes = 366, MalScore = 7.9, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/3/40451l.jpg", Synopsis = "A teenager gains the powers of a Soul Reaper and protects the living from evil spirits." },
                new AnimeDatabase { Id = 17, Title = "Fairy Tail", Genre = "Action,Adventure,Comedy,Fantasy", Studio = "A-1 Pictures", Year = 2009, Episodes = 175, MalScore = 7.9, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/5/18179l.jpg", Synopsis = "A wizard guild takes on dangerous missions in a magical world." },
                new AnimeDatabase { Id = 18, Title = "Toradora", Genre = "Comedy,Drama,Romance,School", Studio = "J.C.Staff", Year = 2008, Episodes = 25, MalScore = 8.2, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/13/22128l.jpg", Synopsis = "Two mismatched teens team up to help each other confess their crushes." },
                new AnimeDatabase { Id = 19, Title = "Clannad: After Story", Genre = "Drama,Romance,Slice of Life", Studio = "Kyoto Animation", Year = 2008, Episodes = 24, MalScore = 9.0, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1299/110774l.jpg", Synopsis = "A delinquent and a girl form bonds that change their lives forever." },
                new AnimeDatabase { Id = 20, Title = "Anohana", Genre = "Drama,Romance,Supernatural", Studio = "A-1 Pictures", Year = 2011, Episodes = 11, MalScore = 8.6, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1887/117644l.jpg", Synopsis = "A group of childhood friends reunite after the ghost of their dead friend appears." },
                new AnimeDatabase { Id = 21, Title = "Sword Art Online: Alicization", Genre = "Action,Adventure,Fantasy,Romance", Studio = "A-1 Pictures", Year = 2018, Episodes = 24, MalScore = 7.8, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1221/96022l.jpg", Synopsis = "Kirito wakes up in an Underworld and must uncover the truth." },
                new AnimeDatabase { Id = 22, Title = "The Promised Neverland", Genre = "Horror,Mystery,Sci-Fi,Thriller", Studio = "CloverWorks", Year = 2019, Episodes = 12, MalScore = 8.6, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1935/99703l.jpg", Synopsis = "Orphaned children discover their idyllic home hides a dark secret." },
                new AnimeDatabase { Id = 23, Title = "Mob Psycho 100", Genre = "Action,Comedy,Supernatural", Studio = "Bones", Year = 2016, Episodes = 12, MalScore = 8.6, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1171/109222l.jpg", Synopsis = "An overpowered psychic boy navigates life while trying to remain emotionally stable." },
                new AnimeDatabase { Id = 24, Title = "Oregairu", Genre = "Comedy,Drama,Romance,School", Studio = "Brain's Base", Year = 2013, Episodes = 13, MalScore = 8.0, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1255/82685l.jpg", Synopsis = "A cynical loner joins a club for helping others and questions social norms." },
                new AnimeDatabase { Id = 25, Title = "Sword Art Online: Progressive", Genre = "Action,Adventure,Fantasy,Romance", Studio = "A-1 Pictures", Year = 2021, Episodes = 1, MalScore = 7.9, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1500/103005l.jpg", Synopsis = "A retelling of SAO from Asuna's perspective, floor by floor." },
                new AnimeDatabase { Id = 26, Title = "Blue Period", Genre = "Drama,School,Slice of Life", Studio = "Seven Arcs", Year = 2021, Episodes = 12, MalScore = 8.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1441/99991l.jpg", Synopsis = "A delinquent discovers his passion for art and strives to enter Tokyo University of the Arts." },
                new AnimeDatabase { Id = 27, Title = "Mushishi", Genre = "Adventure,Fantasy,Mystery,Slice of Life,Supernatural", Studio = "Artland", Year = 2005, Episodes = 26, MalScore = 8.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/3/67177l.jpg", Synopsis = "A traveler investigates mysterious lifeforms called Mushi that affect people's lives." },
                new AnimeDatabase { Id = 28, Title = "Samurai Champloo", Genre = "Action,Adventure,Comedy,Historical", Studio = "Manglobe", Year = 2004, Episodes = 26, MalScore = 8.5, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1521/99674l.jpg", Synopsis = "Two skilled samurai and a girl search for a specific samurai who smells of sunflowers." },
                new AnimeDatabase { Id = 29, Title = "Parasyte", Genre = "Action,Horror,Psychological,Sci-Fi", Studio = "Madhouse", Year = 2014, Episodes = 24, MalScore = 8.4, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/3/73474l.jpg", Synopsis = "A teen's hand is taken over by an alien parasite and they must coexist." },
                new AnimeDatabase { Id = 30, Title = "Tokyo Ghoul", Genre = "Action,Horror,Mystery,Supernatural", Studio = "Pierrot", Year = 2014, Episodes = 12, MalScore = 7.8, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/5/64449l.jpg", Synopsis = "A college student becomes half-ghoul after a chance encounter and must survive in both worlds." },
                new AnimeDatabase { Id = 31, Title = "Sword Art Online II", Genre = "Action,Adventure,Fantasy,Romance", Studio = "A-1 Pictures", Year = 2014, Episodes = 24, MalScore = 7.3, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1286/99889l.jpg", Synopsis = "Kirito enters a new VR game to investigate a mysterious death." },
                new AnimeDatabase { Id = 32, Title = "A Silent Voice", Genre = "Drama,Romance,School", Studio = "Kyoto Animation", Year = 2016, Episodes = 1, MalScore = 8.9, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1988/119581l.jpg", Synopsis = "A former bully seeks redemption by reconnecting with a deaf girl he once tormented." },
                new AnimeDatabase { Id = 33, Title = "Your Name", Genre = "Drama,Romance,Supernatural", Studio = "CoMix Wave Films", Year = 2016, Episodes = 1, MalScore = 8.9, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1286/99889l.jpg", Synopsis = "Two teenagers mysteriously swap bodies and try to find each other." },
                new AnimeDatabase { Id = 34, Title = "Weathering with You", Genre = "Drama,Fantasy,Romance", Studio = "CoMix Wave Films", Year = 2019, Episodes = 1, MalScore = 8.5, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1171/109222l.jpg", Synopsis = "A runaway boy meets a girl with the power to stop rain in Tokyo." },
                new AnimeDatabase { Id = 35, Title = "Given", Genre = "Drama,Music,Romance,Slice of Life", Studio = "Lerche", Year = 2019, Episodes = 11, MalScore = 8.4, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1500/103005l.jpg", Synopsis = "A guitarist meets a boy with a broken guitar and a haunted past." },
                new AnimeDatabase { Id = 36, Title = "Nana", Genre = "Drama,Music,Romance,Slice of Life", Studio = "Madhouse", Year = 2006, Episodes = 47, MalScore = 8.5, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1521/99674l.jpg", Synopsis = "Two girls named Nana meet on a train and become roommates in Tokyo." },
                new AnimeDatabase { Id = 37, Title = "Ouran High School Host Club", Genre = "Comedy,Drama,Romance,School", Studio = "Bones", Year = 2006, Episodes = 26, MalScore = 8.2, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1887/117644l.jpg", Synopsis = "A scholarship student accidentally breaks a vase and must join a host club to repay the debt." },
                new AnimeDatabase { Id = 38, Title = "Fruits Basket", Genre = "Comedy,Drama,Romance,Slice of Life,Supernatural", Studio = "TMS Entertainment", Year = 2019, Episodes = 25, MalScore = 8.5, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1441/99991l.jpg", Synopsis = "A girl discovers a family secret — its members transform into Chinese zodiac animals." },
                new AnimeDatabase { Id = 39, Title = "Kuroko's Basketball", Genre = "Comedy,Sports", Studio = "Production I.G", Year = 2012, Episodes = 25, MalScore = 8.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1988/119581l.jpg", Synopsis = "A phantom player joins a high school basketball team to take on the legendary Generation of Miracles." },
                new AnimeDatabase { Id = 40, Title = "Free!", Genre = "Comedy,Sports,Slice of Life", Studio = "Kyoto Animation", Year = 2013, Episodes = 12, MalScore = 7.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1337/99013l.jpg", Synopsis = "A group of childhood friends reunite to form a competitive swim club." },
                new AnimeDatabase { Id = 41, Title = "Berserk", Genre = "Action,Adventure,Drama,Historical,Horror,Supernatural", Studio = "OLM", Year = 1997, Episodes = 25, MalScore = 8.6, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/3/40451l.jpg", Synopsis = "A lone mercenary joins a band of warriors and faces supernatural evil." },
                new AnimeDatabase { Id = 42, Title = "Trigun", Genre = "Action,Adventure,Comedy,Sci-Fi", Studio = "Madhouse", Year = 1998, Episodes = 26, MalScore = 8.2, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/6/73245l.jpg", Synopsis = "A legendary gunman with a huge bounty wanders a desert planet spreading peace." },
                new AnimeDatabase { Id = 43, Title = "Fullmetal Alchemist (2003)", Genre = "Action,Adventure,Drama,Fantasy", Studio = "Bones", Year = 2003, Episodes = 51, MalScore = 8.0, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1286/99889l.jpg", Synopsis = "Two brothers use alchemy to try to restore their bodies after a failed ritual." },
                new AnimeDatabase { Id = 44, Title = "Gurren Lagann", Genre = "Action,Adventure,Comedy,Mecha,Sci-Fi", Studio = "Gainax", Year = 2007, Episodes = 27, MalScore = 8.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1171/109222l.jpg", Synopsis = "A boy from an underground village leads humanity to fight against oppressive overlords." },
                new AnimeDatabase { Id = 45, Title = "Noragami", Genre = "Action,Adventure,Comedy,Supernatural", Studio = "Bones", Year = 2014, Episodes = 12, MalScore = 7.9, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1500/103005l.jpg", Synopsis = "A minor god tries to build his own shrine and gets entangled with a girl's soul." },
                new AnimeDatabase { Id = 46, Title = "Akame ga Kill!", Genre = "Action,Adventure,Drama,Fantasy", Studio = "White Fox", Year = 2014, Episodes = 24, MalScore = 7.5, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1521/99674l.jpg", Synopsis = "A naive country boy joins an assassin group fighting against a corrupt empire." },
                new AnimeDatabase { Id = 47, Title = "Assassination Classroom", Genre = "Action,Comedy,School,Sci-Fi", Studio = "Lerche", Year = 2015, Episodes = 22, MalScore = 8.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1988/119581l.jpg", Synopsis = "Students must assassinate their teacher who threatens to destroy the Earth." },
                new AnimeDatabase { Id = 48, Title = "The Rising of the Shield Hero", Genre = "Action,Adventure,Drama,Fantasy", Studio = "Kinema Citrus", Year = 2019, Episodes = 25, MalScore = 8.1, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1337/99013l.jpg", Synopsis = "A falsely accused hero must prove himself while protecting his world." },
                new AnimeDatabase { Id = 49, Title = "Sword Art Online: Alicization – War of Underworld", Genre = "Action,Adventure,Fantasy,Romance", Studio = "A-1 Pictures", Year = 2019, Episodes = 23, MalScore = 7.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/1441/99991l.jpg", Synopsis = "The Underworld faces a massive invasion while Kirito remains unconscious." },
                new AnimeDatabase { Id = 50, Title = "Black Butler", Genre = "Action,Comedy,Fantasy,Historical,Mystery,Supernatural", Studio = "A-1 Pictures", Year = 2008, Episodes = 24, MalScore = 7.7, CoverImageUrl = "https://cdn.myanimelist.net/images/anime/6/86733l.jpg", Synopsis = "A young nobleman makes a contract with a demon butler to avenge his family." }
            );
        }
    }
}