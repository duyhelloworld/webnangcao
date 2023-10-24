using System.ComponentModel.DataAnnotations;

namespace webnangcao.Models.Securities;

public class SignupModel 
{
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [StringLength(40)]
    public string UserName  { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string? Address { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
}