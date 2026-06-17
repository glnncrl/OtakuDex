using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtakuDex.Models;

namespace OtakuDex.Controllers
{
    public class CollectionController : Controller
    {
        private readonly AppDbContext _context;

        public CollectionController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var collections = await _context.Collections
                .Include(c => c.CollectionItems)
                    .ThenInclude(ci => ci.Anime)
                .OrderByDescending(c => c.DateCreated)
                .ToListAsync();
            return View(collections);
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var collection = await _context.Collections
                .Include(c => c.CollectionItems)
                    .ThenInclude(ci => ci.Anime)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (collection == null) return NotFound();
            return View(collection);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                collection.DateCreated = DateTime.Now;
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null) return NotFound();
            return View(collection);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateCreated")] Collection collection)
        {
            if (id != collection.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var collection = await _context.Collections
                .Include(c => c.CollectionItems)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (collection == null) return NotFound();
            return View(collection);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null) _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ADD ANIME TO COLLECTION GET
        public async Task<IActionResult> AddAnime(int id)
        {
            var collection = await _context.Collections
                .Include(c => c.CollectionItems)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (collection == null) return NotFound();

            var existingAnimeIds = collection.CollectionItems.Select(ci => ci.AnimeId).ToList();
            var animes = await _context.Animes
                .Where(a => !existingAnimeIds.Contains(a.Id))
                .OrderBy(a => a.Title)
                .ToListAsync();

            ViewBag.Collection = collection;
            ViewBag.Animes = animes;
            return View();
        }

        // ADD ANIME TO COLLECTION POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnime(int collectionId, int animeId)
        {
            var exists = await _context.CollectionItems
                .AnyAsync(ci => ci.CollectionId == collectionId && ci.AnimeId == animeId);

            if (!exists)
            {
                var item = new CollectionItem
                {
                    CollectionId = collectionId,
                    AnimeId = animeId,
                    DateAdded = DateTime.Now
                };
                _context.Add(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = collectionId });
        }

        // REMOVE ANIME FROM COLLECTION
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAnime(int collectionId, int animeId)
        {
            var item = await _context.CollectionItems
                .FirstOrDefaultAsync(ci => ci.CollectionId == collectionId && ci.AnimeId == animeId);
            if (item != null)
            {
                _context.CollectionItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = collectionId });
        }
    }
}