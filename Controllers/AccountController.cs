using Microsoft.AspNetCore.Mvc;
using IdentityWork.ViewModels;
using IdentityWork.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityWork.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new AccountLoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index","Account");
                    }
                }else
                {
                    ModelState.AddModelError(string.Empty, "Login yoki parol hato");
                }
            }
            return View(model);
        }
    }
}
