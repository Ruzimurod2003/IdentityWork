using Microsoft.AspNetCore.Identity;

namespace IdentityWork.ViewModels;

public class EditRolesViewModel
{
    public string? UserId { get; set; }

    public string? UserEmail { get; set; }

    public List<IdentityRole>? AllRoles { get; set; }

    public IList<string>? UserRoles { get; set; }

    public EditRolesViewModel()
    {
        AllRoles = new List<IdentityRole>();
        UserRoles = new List<string>();
    }
}
