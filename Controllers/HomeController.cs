using Microsoft.AspNetCore.Mvc;
using IdentityWork.ViewModels;
using IdentityWork.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityWork.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> userManager;

        public HomeController(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var obj = userManager.Users.ToList();
            return View(obj);
        }
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(HomeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.phoneNumber,
                    DateCreated = DateTime.Now
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            HomeEditViewModel model = new HomeEditViewModel();
            foreach (var item in userManager.Users)
            {
                if (item.Id == id)
                {
                    model.Id = item.Id;
                    model.UserName = item.UserName;
                    model.Email = item.Email;
                    model.phoneNumber = item.PhoneNumber;
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(HomeEditViewModel model)
        {
            var _passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

            User user = await userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.phoneNumber;
                user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);

                var Result = await userManager.UpdateAsync(user);
                if (Result.Succeeded)
                {
                   return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index", "Home");
        }
    }

}
