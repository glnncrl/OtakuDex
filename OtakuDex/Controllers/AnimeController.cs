using OtakuDex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OtakuDex.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AppDbContext _context;

        public AnimeController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index(string searchString, string statusFilter, string sortOrder)
        {
            ViewData["TitleSort"] = sortOrder == "title_asc" ? "title_desc" : "title_asc";
            ViewData["RatingSort"] = sortOrder == "rating_asc" ? "rating_desc" : "rating_asc";
            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentStatus"] = statusFilter;
            ViewData["CurrentSort"] = sortOrder;

            var animes = _context.Animes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                animes = animes.Where(a => a.Title.Contains(searchString) ||
                                          (a.Genre != null && a.Genre.Contains(searchString)) ||
                                          (a.Studio != null && a.Studio.Contains(searchString)));

            if (!string.IsNullOrEmpty(statusFilter))
                animes = animes.Where(a => a.Status == statusFilter);

            animes = sortOrder switch
            {
                "title_asc" => animes.OrderBy(a => a.Title),
                "title_desc" => animes.OrderByDescending(a => a.Title),
                "rating_asc" => animes.OrderBy(a => a.Rating),
                "rating_desc" => animes.OrderByDescending(a => a.Rating),
                "year_desc" => animes.OrderByDescending(a => a.Year),
                "year_asc" => animes.OrderBy(a => a.Year),
                _ => animes.OrderByDescending(a => a.DateAdded)
            };

            var all = await _context.Animes.ToListAsync();
            ViewData["TotalCount"] = all.Count;
            ViewData["WatchingCount"] = all.Count(a => a.Status == "Watching");
            ViewData["CompletedCount"] = all.Count(a => a.Status == "Completed");
            ViewData["PlanCount"] = all.Count(a => a.Status == "Plan to Watch");
            ViewData["DroppedCount"] = all.Count(a => a.Status == "Dropped");
            ViewData["AvgRating"] = all.Where(a => a.Rating.HasValue).Any()
                                         ? all.Where(a => a.Rating.HasValue).Average(a => a.Rating!.Value).ToString("0.0")
                                         : "—";

            return View(await animes.ToListAsync());
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var anime = await _context.Animes
                .Include(a => a.Reviews)
                .Include(a => a.AnimeGenres)
                    .ThenInclude(ag => ag.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anime == null) return NotFound();
            return View(anime);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Genre,Episodes,Status,Rating,Year,Studio,Synopsis,CoverImageUrl,Notes")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                anime.DateAdded = DateTime.Now;
                _context.Add(anime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var anime = await _context.Animes.FindAsync(id);
            if (anime == null) return NotFound();
            return View(anime);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Episodes,Status,Rating,Year,Studio,Synopsis,CoverImageUrl,Notes,DateAdded")] Anime anime)
        {
            if (id != anime.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Animes.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var anime = await _context.Animes.FirstOrDefaultAsync(m => m.Id == id);
            if (anime == null) return NotFound();
            return View(anime);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime != null) _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}