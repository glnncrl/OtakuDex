using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtakuDex.Models;

namespace OtakuDex.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var animes = await _context.Animes
                .Include(a => a.Reviews)
                .Include(a => a.AnimeGenres)
                    .ThenInclude(ag => ag.Genre)
                .ToListAsync();

            var reviews = await _context.Reviews.ToListAsync();
            var collections = await _context.Collections
                .Include(c => c.CollectionItems)
                .ToListAsync();
            var genres = await _context.Genres
                .Include(g => g.AnimeGenres)
                .ToListAsync();

            // ── Key Stats ──
            ViewBag.TotalAnime = animes.Count;
            ViewBag.TotalReviews = reviews.Count;
            ViewBag.TotalCollections = collections.Count;
            ViewBag.TotalGenres = genres.Count;
            ViewBag.AvgRating = animes.Where(a => a.Rating.HasValue).Any()
                                        ? Math.Round(animes.Where(a => a.Rating.HasValue).Average(a => a.Rating!.Value), 1)
                                        : 0;
            ViewBag.CompletionRate = animes.Any()
                                        ? Math.Round((double)animes.Count(a => a.Status == "Completed") / animes.Count * 100, 1)
                                        : 0;
            ViewBag.TotalEpisodes = animes.Where(a => a.Episodes.HasValue).Sum(a => a.Episodes!.Value);

            // ── Watch Status Distribution ──
            ViewBag.StatusLabels = new[] { "Watching", "Completed", "Plan to Watch", "On Hold", "Dropped" };
            ViewBag.StatusCounts = new[] {
                animes.Count(a => a.Status == "Watching"),
                animes.Count(a => a.Status == "Completed"),
                animes.Count(a => a.Status == "Plan to Watch"),
                animes.Count(a => a.Status == "On Hold"),
                animes.Count(a => a.Status == "Dropped"),
            };

            // ── Rating Distribution ──
            ViewBag.RatingLabels = Enumerable.Range(1, 10).Select(i => i.ToString()).ToArray();
            ViewBag.RatingCounts = Enumerable.Range(1, 10)
                .Select(i => animes.Count(a => a.Rating == i))
                .ToArray();

            // ── Top Genres ──
            var topGenres = genres
                .OrderByDescending(g => g.AnimeGenres.Count)
                .Take(8)
                .ToList();
            ViewBag.GenreLabels = topGenres.Select(g => g.Name).ToArray();
            ViewBag.GenreCounts = topGenres.Select(g => g.AnimeGenres.Count).ToArray();

            // ── Anime Added Over Time (last 12 months) ──
            var now = DateTime.Now;
            var monthLabels = Enumerable.Range(0, 12)
                .Select(i => now.AddMonths(-11 + i))
                .Select(d => d.ToString("MMM yyyy"))
                .ToArray();
            var monthCounts = Enumerable.Range(0, 12)
                .Select(i => {
                    var month = now.AddMonths(-11 + i);
                    return animes.Count(a =>
                        a.DateAdded.Year == month.Year &&
                        a.DateAdded.Month == month.Month);
                })
                .ToArray();
            ViewBag.MonthLabels = monthLabels;
            ViewBag.MonthCounts = monthCounts;

            // ── Top Rated Anime ──
            ViewBag.TopRated = animes
                .Where(a => a.Rating.HasValue)
                .OrderByDescending(a => a.Rating)
                .Take(5)
                .ToList();

            // ── Most Reviewed Anime ──
            ViewBag.MostReviewed = animes
                .Where(a => a.Reviews.Any())
                .OrderByDescending(a => a.Reviews.Count)
                .Take(5)
                .ToList();

            // ── Studio Stats ──
            ViewBag.TopStudios = animes
                .Where(a => !string.IsNullOrEmpty(a.Studio))
                .GroupBy(a => a.Studio)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new { Studio = g.Key, Count = g.Count() })
                .ToList();

            return View();
        }
    }
}