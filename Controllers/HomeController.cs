using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectASP.Models;
using System.Diagnostics;

namespace ProjectASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)

        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync("thibo.2004@icloud.com");

            if (user != null)
            {
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    Console.WriteLine(user + " is now Administrator");
                }
                else
                {
                    Console.WriteLine(user + " Failed to make admin");
                }

                await LogUserRole(user);
            }

            return View();
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task LogUserRole(ApplicationUser user)
        {
            var userRole = "Gebruiker"; // Standaardrol
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                userRole = "Admin";
            }

            _logger.LogInformation($"User {user.Email} has role: {userRole}");
        }
    }
}
