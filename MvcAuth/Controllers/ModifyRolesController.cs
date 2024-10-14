using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MvcAuth.Controllers
{
    public class ModifyRolesController : Controller
    {
        private readonly ILogger<ModifyRolesController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public ModifyRolesController(ILogger<ModifyRolesController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddToRoleAdmin()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRole()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            return Content(string.Join(", ",roles));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveFromRoleAdmin()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddToRoleUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Index","Home");
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveFromRoleUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _userManager.RemoveFromRoleAsync(user, "User");
            return RedirectToAction("Index","Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}