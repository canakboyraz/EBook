using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.UI.Models.Account;

namespace MVCFinalProje.UI.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                await Console.Out.WriteLineAsync("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }
            var checkPassword = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!checkPassword.Succeeded)
            {
                await Console.Out.WriteLineAsync("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null)
            {
                await Console.Out.WriteLineAsync("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }

            return RedirectToAction("Index","Home", new {Area = userRole[0].ToString() }); 
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
            
        }
    }
}
