using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ProjectASP.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Matches.ToListAsync());
        }

        // Hier voeg je andere acties toe zoals Create, Edit, Delete
    }
}
