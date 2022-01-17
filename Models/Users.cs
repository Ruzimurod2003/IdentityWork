using Microsoft.AspNetCore.Identity;

namespace IdentityWork.Models;
public class User : IdentityUser
{
    public DateTime DateCreated { get; set; }
}