using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index(string ronde)
        {
            var matches = !string.IsNullOrWhiteSpace(ronde) ?
                await _context.Matches.Where(m => m.Ronde == ronde).ToListAsync() :
                await _context.Matches.ToListAsync();

            return View(matches);
        }

        // Action voor het ophalen van gebruikersnamen uit de database
        public IActionResult GetUsers()
        {
            var users = _context.Users.Select(u => new { u.Id, u.FirstName, u.LastName }).ToList();
            return Json(users);
        }

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
                    Ronde = model.Ronde,
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

        [HttpPost]
        public async Task<IActionResult> UpdateScores(int id, [FromBody] ScoreUpdateViewModel scoreUpdate)
        {
            if (id != scoreUpdate.MatchId)
            {
                return BadRequest("Mismatched match ID.");
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound("Match not found.");
            }

            try
            {
                match.ScorePlayerOne = scoreUpdate.ScorePlayerOne;
                match.ScorePlayerTwo = scoreUpdate.ScorePlayerTwo;
                match.IsPlayed = true;
                match.IsApproved = false;

                _context.Entry(match).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating match: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApproveScores(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound("Match not found.");
            }

            match.IsApproved = true;
            _context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }




        // Hier voeg je andere acties toe zoals Edit, Delete, enzovoort
    }
}
