using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        // Action voor het ophalen van gebruikersnamen uit de database
        public IActionResult GetUsers()
        {
            var users = _context.Users.Select(u => new { u.Id, u.FirstName, u.LastName }).ToList();
            return Json(users);
        }

        // GET: Matches/Add
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Match model)
        {
            if (ModelState.IsValid)
            {
                var match = new Match
                {
                    PlayerOne = model.PlayerOne,
                    PlayerTwo = model.PlayerTwo,
                    ScorePlayerOne = model.ScorePlayerOne,
                    ScorePlayerTwo = model.ScorePlayerTwo,
                    IsPlayed = model.IsPlayed,
                    Datum = model.Datum
                };

                _context.Matches.Add(match);
                await _context.SaveChangesAsync();
                return Ok(); // Return een succesvol statuscode
            }
            return BadRequest(); // Return een fout statuscode
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return Ok();
        }


        // Hier voeg je andere acties toe zoals Edit, Delete, enzovoort
    }
}
