using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtakuDex.Migrations
{
    public partial class AddAnimeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeDatabase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Studio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Episodes = table.Column<int>(type: "int", nullable: false),
                    MalScore = table.Column<double>(type: "float", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDatabase", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AnimeDatabase",
                columns: new[] { "Id", "Title", "Genre", "Studio", "Year", "Episodes", "MalScore", "CoverImageUrl", "Synopsis" },
                values: new object[,]
                {
                    { 1, "Steins;Gate", "Sci-Fi,Thriller,Drama", "White Fox", 2011, 24, 9.1, "https://cdn.myanimelist.net/images/anime/5/73199l.jpg", "A self-proclaimed mad scientist discovers time travel and must face its consequences." },
                    { 2, "Hunter x Hunter (2011)", "Action,Adventure,Fantasy", "Madhouse", 2011, 148, 9.0, "https://cdn.myanimelist.net/images/anime/1337/99013l.jpg", "A young boy searches for his missing father in a world of hunters." },
                    { 3, "Neon Genesis Evangelion", "Action,Sci-Fi,Drama,Psychological", "Gainax", 1995, 26, 8.5, "https://cdn.myanimelist.net/images/anime/1314/108888l.jpg", "Teenagers pilot giant mechs to protect humanity from mysterious beings." },
                    { 4, "Cowboy Bebop", "Action,Sci-Fi,Drama", "Sunrise", 1998, 26, 8.8, "https://cdn.myanimelist.net/images/anime/4/19644l.jpg", "A ragtag crew of bounty hunters travel the solar system." },
                    { 5, "Death Note", "Thriller,Supernatural,Drama,Psychological", "Madhouse", 2006, 37, 8.6, "https://cdn.myanimelist.net/images/anime/9/9453l.jpg", "A genius student finds a notebook that can kill anyone whose name is written in it." },
                    { 6, "Code Geass", "Action,Sci-Fi,Drama,Mecha", "Sunrise", 2006, 25, 8.7, "https://cdn.myanimelist.net/images/anime/5/50331l.jpg", "An exiled prince gains the power to command anyone and leads a rebellion." },
                    { 7, "Made in Abyss", "Adventure,Drama,Fantasy,Sci-Fi", "Kinema Citrus", 2017, 13, 8.7, "https://cdn.myanimelist.net/images/anime/6/86733l.jpg", "A girl and a robot boy descend into a mysterious and dangerous abyss." },
                    { 8, "Re:Zero", "Drama,Fantasy,Psychological,Thriller", "White Fox", 2016, 25, 8.3, "https://cdn.myanimelist.net/images/anime/1522/128039l.jpg", "A boy is transported to a fantasy world and discovers he revives upon death." },
                    { 9, "No Game No Life", "Comedy,Fantasy", "Madhouse", 2014, 12, 8.1, "https://cdn.myanimelist.net/images/anime/1074/111944l.jpg", "Genius gamer siblings are transported to a world where everything is decided by games." },
                    { 10, "Overlord", "Action,Adventure,Fantasy", "Madhouse", 2015, 13, 7.9, "https://cdn.myanimelist.net/images/anime/7/88211l.jpg", "A player is stuck in an RPG as his character and builds his own empire." },
                    { 11, "That Time I Got Reincarnated as a Slime", "Action,Adventure,Comedy,Fantasy", "8bit", 2018, 24, 8.1, "https://cdn.myanimelist.net/images/anime/1506/117736l.jpg", "A man reincarnates as a slime in a fantasy world and builds his own nation." },
                    { 12, "The Promised Neverland", "Horror,Mystery,Sci-Fi,Thriller", "CloverWorks", 2019, 12, 8.6, "https://cdn.myanimelist.net/images/anime/1935/99703l.jpg", "Orphaned children discover their idyllic home hides a dark secret." },
                    { 13, "Mob Psycho 100", "Action,Comedy,Supernatural", "Bones", 2016, 12, 8.6, "https://cdn.myanimelist.net/images/anime/1171/109222l.jpg", "An overpowered psychic boy navigates life while trying to remain emotionally stable." },
                    { 14, "Toradora", "Comedy,Drama,Romance,School", "J.C.Staff", 2008, 25, 8.2, "https://cdn.myanimelist.net/images/anime/13/22128l.jpg", "Two mismatched teens team up to help each other confess their crushes." },
                    { 15, "Clannad: After Story", "Drama,Romance,Slice of Life", "Kyoto Animation", 2008, 24, 9.0, "https://cdn.myanimelist.net/images/anime/1299/110774l.jpg", "A delinquent and a girl form bonds that change their lives forever." },
                    { 16, "Anohana", "Drama,Romance,Supernatural", "A-1 Pictures", 2011, 11, 8.6, "https://cdn.myanimelist.net/images/anime/1887/117644l.jpg", "A group of childhood friends reunite after the ghost of their dead friend appears." },
                    { 17, "A Silent Voice", "Drama,Romance,School", "Kyoto Animation", 2016, 1, 8.9, "https://cdn.myanimelist.net/images/anime/1988/119581l.jpg", "A former bully seeks redemption by reconnecting with a deaf girl he once tormented." },
                    { 18, "Your Name", "Drama,Romance,Supernatural", "CoMix Wave Films", 2016, 1, 8.9, "https://cdn.myanimelist.net/images/anime/1286/99889l.jpg", "Two teenagers mysteriously swap bodies and try to find each other." },
                    { 19, "Weathering with You", "Drama,Fantasy,Romance", "CoMix Wave Films", 2019, 1, 8.5, "https://cdn.myanimelist.net/images/anime/1171/109222l.jpg", "A runaway boy meets a girl with the power to stop rain in Tokyo." },
                    { 20, "Parasyte", "Action,Horror,Psychological,Sci-Fi", "Madhouse", 2014, 24, 8.4, "https://cdn.myanimelist.net/images/anime/3/73474l.jpg", "A teen's hand is taken over by an alien parasite and they must coexist." },
                    { 21, "Tokyo Ghoul", "Action,Horror,Mystery,Supernatural", "Pierrot", 2014, 12, 7.8, "https://cdn.myanimelist.net/images/anime/5/64449l.jpg", "A college student becomes half-ghoul after a chance encounter." },
                    { 22, "Gurren Lagann", "Action,Adventure,Comedy,Mecha,Sci-Fi", "Gainax", 2007, 27, 8.7, "https://cdn.myanimelist.net/images/anime/1171/109222l.jpg", "A boy from an underground village leads humanity to fight against oppressive overlords." },
                    { 23, "Samurai Champloo", "Action,Adventure,Comedy,Historical", "Manglobe", 2004, 26, 8.5, "https://cdn.myanimelist.net/images/anime/1521/99674l.jpg", "Two skilled samurai and a girl search for a specific samurai who smells of sunflowers." },
                    { 24, "Mushishi", "Adventure,Fantasy,Mystery,Slice of Life,Supernatural", "Artland", 2005, 26, 8.7, "https://cdn.myanimelist.net/images/anime/3/67177l.jpg", "A traveler investigates mysterious lifeforms called Mushi that affect peoples lives." },
                    { 25, "Assassination Classroom", "Action,Comedy,School,Sci-Fi", "Lerche", 2015, 22, 8.1, "https://cdn.myanimelist.net/images/anime/1988/119581l.jpg", "Students must assassinate their teacher who threatens to destroy the Earth." },
                    { 26, "The Rising of the Shield Hero", "Action,Adventure,Drama,Fantasy", "Kinema Citrus", 2019, 25, 8.1, "https://cdn.myanimelist.net/images/anime/1337/99013l.jpg", "A falsely accused hero must prove himself while protecting his world." },
                    { 27, "Noragami", "Action,Adventure,Comedy,Supernatural", "Bones", 2014, 12, 7.9, "https://cdn.myanimelist.net/images/anime/1500/103005l.jpg", "A minor god tries to build his own shrine and gets entangled with a girls soul." },
                    { 28, "Fruits Basket", "Comedy,Drama,Romance,Slice of Life,Supernatural", "TMS Entertainment", 2019, 25, 8.5, "https://cdn.myanimelist.net/images/anime/1441/99991l.jpg", "A girl discovers a family secret — its members transform into Chinese zodiac animals." },
                    { 29, "Ouran High School Host Club", "Comedy,Drama,Romance,School", "Bones", 2006, 26, 8.2, "https://cdn.myanimelist.net/images/anime/1887/117644l.jpg", "A scholarship student accidentally breaks a vase and must join a host club to repay the debt." },
                    { 30, "Berserk", "Action,Adventure,Drama,Historical,Horror,Supernatural", "OLM", 1997, 25, 8.6, "https://cdn.myanimelist.net/images/anime/3/40451l.jpg", "A lone mercenary joins a band of warriors and faces supernatural evil." },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AnimeDatabase");
        }
    }
}