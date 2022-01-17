using IdentityWork.Models;
using IdentityWork.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWork.Controllers;

public class RolesController : Controller
{
    public RoleManager<IdentityRole> roleManager { get; private set; }
    public UserManager<User> userManager { get; private set; }

    public RolesController(RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager)
    {
        roleManager = _roleManager;
        userManager = _userManager;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View(roleManager.Roles.ToList());
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(RolesCreateViewModel viewModel)
    {
        if (!string.IsNullOrEmpty(viewModel.Name))
        {
            var result = await roleManager.CreateAsync(new IdentityRole(viewModel.Name));
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
        }
        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);
        if (role != null)
        {
            await roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string userId)
    {
        // получаем пользователя
        User user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // получем список ролей пользователя
            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = roleManager.Roles.ToList();
            EditRolesViewModel model = new EditRolesViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(model);
        }

        return NotFound();
    }

    public IActionResult UserList() => View(userManager.Users.ToList());

    [HttpPost]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        // получаем пользователя
        User user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // получем список ролей пользователя
            var userRoles = await userManager.GetRolesAsync(user);
            // получаем все роли
            var allRoles = roleManager.Roles.ToList();
            // получаем список ролей, которые были добавлены
            var addedRoles = roles.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(roles);

            await userManager.AddToRolesAsync(user, addedRoles);

            await userManager.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("UserList");
        }

        return NotFound();
    }
}
