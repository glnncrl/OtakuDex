using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtakuDex.Models;

namespace OtakuDex.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE GET
        public async Task<IActionResult> Create(int animeId)
        {
            var anime = await _context.Animes.FindAsync(animeId);
            if (anime == null) return NotFound();
            ViewBag.Anime = anime;
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimeId,ReviewerName,Content,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.DatePosted = DateTime.Now;
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Anime", new { id = review.AnimeId });
            }
            var anime = await _context.Animes.FindAsync(review.AnimeId);
            ViewBag.Anime = anime;
            return View(review);
        }

        // DELETE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                int animeId = review.AnimeId;
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Anime", new { id = animeId });
            }
            return RedirectToAction("Index", "Anime");
        }
    }
}