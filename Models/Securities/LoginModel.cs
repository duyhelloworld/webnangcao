using System.ComponentModel.DataAnnotations;

namespace webnangcao.Models.Securities;

public class LoginModel
{
    [Required]
    [StringLength(40)]
    public string UserName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "Nhớ cho lần đăng nhập sau?")]
    public bool RememberMe { get; set; }
}