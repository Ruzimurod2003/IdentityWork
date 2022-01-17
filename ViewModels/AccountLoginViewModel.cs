using System.ComponentModel.DataAnnotations;

namespace IdentityWork.ViewModels;

public class AccountLoginViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name ="Please enter your email")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Please enter your password")]
    public string? Password { get; set; }

    public bool RememberMe { get; set; } 

    public string? ReturnUrl { get; set; }
}
