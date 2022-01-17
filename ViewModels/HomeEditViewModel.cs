using System.ComponentModel.DataAnnotations;

namespace IdentityWork.ViewModels;

public class HomeEditViewModel
{

    public string? Id { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Please enter your email")]
    public string? Email { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Нет это слово очен низкий")]
    [Display(Name = "Please enter your user name")]
    public string? UserName { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Please enter your password")]
    [Required]
    public string? NewPassword { get; set; }

    [Required]
    [Display(Name = "Please enter your phone number")]
    public string? phoneNumber { get; set; }
}
