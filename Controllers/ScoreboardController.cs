using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ProjectASP.Controllers
{
    public class ScoreboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Logica voor het ophalen van de scores en deze doorsturen naar de view
            return View(await _context.Users.ToListAsync()); // Pas aan op basis van je implementatie
        }
    }

}
