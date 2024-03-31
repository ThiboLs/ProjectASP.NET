using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProjectASP.Models;
using System.Linq;
using System.Threading.Tasks;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var wonMatches = _context.Matches.Where(m => m.IsPlayed && m.IsApproved && (m.PlayerOne == user.Id || m.PlayerTwo == user.Id))
                                         .Count(m => m.ScorePlayerOne > m.ScorePlayerTwo || m.ScorePlayerOne < m.ScorePlayerTwo);

        var wonLegs = _context.Matches.Where(m => m.IsPlayed && m.IsApproved && (m.PlayerOne == user.Id || m.PlayerTwo == user.Id))
                                      .Sum(m => m.PlayerOne == user.Id ? m.ScorePlayerOne : m.ScorePlayerTwo);

        var userStatistics = new UserStatistics
        {
            WonMatches = wonMatches,
            WonLegs = wonLegs
        };

        return View(userStatistics);
    }
}
