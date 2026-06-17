using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtakuDex.Models;

namespace OtakuDex.Controllers
{
    public class GenreController : Controller
    {
        private readonly AppDbContext _context;

        public GenreController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genres
                .Include(g => g.AnimeGenres)
                    .ThenInclude(ag => ag.Anime)
                .OrderBy(g => g.Name)
                .ToListAsync();
            return View(genres);
        }

        // DETAILS — show all anime with this genre
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var genre = await _context.Genres
                .Include(g => g.AnimeGenres)
                    .ThenInclude(ag => ag.Anime)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null) return NotFound();
            return View(genre);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicates
                var exists = await _context.Genres
                    .AnyAsync(g => g.Name.ToLower() == genre.Name.ToLower());
                if (exists)
                {
                    ModelState.AddModelError("Name", "This genre already exists.");
                    return View(genre);
                }
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // DELETE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ASSIGN GENRE TO ANIME POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToAnime(int animeId, int genreId)
        {
            var exists = await _context.AnimeGenres
                .AnyAsync(ag => ag.AnimeId == animeId && ag.GenreId == genreId);
            if (!exists)
            {
                _context.AnimeGenres.Add(new AnimeGenre
                {
                    AnimeId = animeId,
                    GenreId = genreId
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Anime", new { id = animeId });
        }

        // REMOVE GENRE FROM ANIME POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromAnime(int animeId, int genreId)
        {
            var ag = await _context.AnimeGenres
                .FirstOrDefaultAsync(ag => ag.AnimeId == animeId && ag.GenreId == genreId);
            if (ag != null)
            {
                _context.AnimeGenres.Remove(ag);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Anime", new { id = animeId });
        }
    }
}