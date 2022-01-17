using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace IdentityWork.Models;
public class ApplicationContext:IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions options):base(options)
    {
        Database.EnsureCreated();
    }
}