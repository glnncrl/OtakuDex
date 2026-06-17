using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtakuDex.Models;

namespace OtakuDex.Controllers
{
    public class RecommendationViewModel
    {
        public AnimeDatabase Anime { get; set; } = null!;
        public int MatchScore { get; set; }
        public List<string> MatchReasons { get; set; } = new();
    }

    public class RecommendationController : Controller
    {
        private readonly AppDbContext _context;

        public RecommendationController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get user's tracked anime
            var trackedAnime = await _context.Animes
                .Include(a => a.AnimeGenres)
                    .ThenInclude(ag => ag.Genre)
                .ToListAsync();

            // Get all anime from the database catalog
            var catalog = await _context.AnimeDatabase.ToListAsync();

            // Get already tracked titles to exclude
            var trackedTitles = trackedAnime
                .Select(a => a.Title.ToLower().Trim())
                .ToHashSet();

            // ── Build user preference profile ──

            // 1. Favorite genres from high-rated anime (rating >= 7)
            var favoriteGenres = trackedAnime
                .Where(a => a.Rating.HasValue && a.Rating >= 7)
                .SelectMany(a => (a.Genre ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(g => g.Trim().ToLower())
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => g.Key)
                .ToList();

            // Also include genre tags from AnimeGenres table
            var taggedGenres = trackedAnime
                .Where(a => a.Rating.HasValue && a.Rating >= 7)
                .SelectMany(a => a.AnimeGenres.Select(ag => ag.Genre?.Name?.ToLower().Trim() ?? ""))
                .Where(g => !string.IsNullOrEmpty(g))
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => g.Key)
                .ToList();

            favoriteGenres = favoriteGenres.Union(taggedGenres).Take(8).ToList();

            // 2. Favorite studios from high-rated anime
            var favoriteStudios = trackedAnime
                .Where(a => a.Rating.HasValue && a.Rating >= 8 && !string.IsNullOrEmpty(a.Studio))
                .GroupBy(a => a.Studio!.ToLower().Trim())
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            // 3. Average rating of user's tracker
            var avgRating = trackedAnime.Where(a => a.Rating.HasValue).Any()
                ? trackedAnime.Where(a => a.Rating.HasValue).Average(a => a.Rating!.Value)
                : 7.0;

            // 4. Preferred era (most common decade)
            var preferredDecade = trackedAnime
                .Where(a => a.Year.HasValue)
                .GroupBy(a => (a.Year!.Value / 10) * 10)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? 2010;

            // ── Score each catalog entry ──
            var recommendations = new List<RecommendationViewModel>();

            foreach (var anime in catalog)
            {
                // Skip already tracked
                if (trackedTitles.Contains(anime.Title.ToLower().Trim()))
                    continue;

                int score = 0;
                var reasons = new List<string>();

                var catalogGenres = anime.Genre
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(g => g.Trim().ToLower())
                    .ToList();

                // Genre match — up to 50 points
                var genreMatches = catalogGenres.Intersect(favoriteGenres).ToList();
                if (genreMatches.Any())
                {
                    int genreScore = Math.Min(genreMatches.Count * 15, 50);
                    score += genreScore;
                    reasons.Add($"Matches your favorite genre{(genreMatches.Count > 1 ? "s" : "")}: {string.Join(", ", genreMatches.Select(g => char.ToUpper(g[0]) + g.Substring(1)))}");
                }

                // Studio match — 20 points
                if (!string.IsNullOrEmpty(anime.Studio) &&
                    favoriteStudios.Contains(anime.Studio.ToLower().Trim()))
                {
                    score += 20;
                    reasons.Add($"From {anime.Studio}, a studio you enjoy");
                }

                // MAL Score bonus — up to 20 points
                if (anime.MalScore >= 9.0) { score += 20; reasons.Add($"Critically acclaimed — MAL score {anime.MalScore}"); }
                else if (anime.MalScore >= 8.5) { score += 15; reasons.Add($"Highly rated — MAL score {anime.MalScore}"); }
                else if (anime.MalScore >= 8.0) { score += 10; reasons.Add($"Well rated — MAL score {anime.MalScore}"); }
                else if (anime.MalScore >= 7.5) { score += 5; }

                // Era match — 10 points
                int animDecade = (anime.Year / 10) * 10;
                if (animDecade == preferredDecade)
                {
                    score += 10;
                    reasons.Add($"From your preferred era ({preferredDecade}s)");
                }

                // Only recommend if score is meaningful
                if (score >= 15)
                {
                    recommendations.Add(new RecommendationViewModel
                    {
                        Anime = anime,
                        MatchScore = Math.Min(score, 100),
                        MatchReasons = reasons
                    });
                }
            }

            // Sort by score descending, take top 12
            var top = recommendations
                .OrderByDescending(r => r.MatchScore)
                .ThenByDescending(r => r.Anime.MalScore)
                .Take(12)
                .ToList();

            // Pass profile to view
            ViewBag.FavoriteGenres = favoriteGenres;
            ViewBag.FavoriteStudios = favoriteStudios;
            ViewBag.TrackedCount = trackedAnime.Count;
            ViewBag.HasEnoughData = trackedAnime.Count(a => a.Rating.HasValue) >= 3;

            return View(top);
        }
    }
}