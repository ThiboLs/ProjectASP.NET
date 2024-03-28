using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Models;


namespace ProjectASP.Controllers
{
    public class ScoreboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreboardController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index(string ronde)
        {
            IEnumerable<Match> matches;

            // Check of de ronde parameter een niet-lege string is
            if (!string.IsNullOrWhiteSpace(ronde))
            {
                // Filter de matches op de gegeven ronde
                matches = await _context.Matches.Where(m => m.Ronde == ronde).ToListAsync();
            }
            else
            {
                // Haal alle matches op als er geen specifieke ronde is opgegeven
                matches = await _context.Matches.ToListAsync();
            }

            return View(matches);
        }
    }

}
