using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProjectASP.Models;
using System.Threading.Tasks;

namespace ProjectASP.Controllers
{
    public class CompleteProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CompleteProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CompleteProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.BirthDate = model.BirthDate;

                    // Update de gebruiker in de database
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        // Stuur de gebruiker door naar een andere pagina na succesvol invullen van het profiel
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        // Als er fouten zijn opgetreden bij het bijwerken van de gebruiker, voeg deze toe aan ModelState
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
