using webnangcao.Models.Securities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using webnangcao.Entities;
using webnangcao.Exceptions;
using webnangcao.Entities.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Services.Impls;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    public async Task<ResponseModel> SignInAsync(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Tài khoản không tồn tại", "Hãy đăng kí trước");
                
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
        if (!result.Succeeded)
        {
            return new ResponseModel()
            {
                IsSucceed = false,
                Data = "Tài khoản hoặc mật khẩu không chính xác"
            };
        }
        var highestRole = ERoleTool.GetHighestRole(await _userManager.GetRolesAsync(user));
        // var refreshToken = await _userManager.GenerateUserTokenAsync(user, "Default", "refresh_token");
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = new SuccessSignupModel()
            {
                UserId = user.Id,
                AccessToken = GenerateJwtToken(user, highestRole),
                // RefreshToken = refreshToken
            }
        };
    }

    public async Task<ResponseModel> SignUpAsync(SignupModel model, ERole erole)
    {
        var user = new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            FullName = model.FullName,
            Address = model.Address
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                switch (err.Code)
                {
                    case "DuplicateUserName":
                        throw new AppException(HttpStatusCode.BadRequest, 
                                $"Tên đăng nhập '{model.UserName}' đã tồn tại", "Hãy thử lại tên khác");
                    case "DuplicateEmail":
                        throw new AppException(HttpStatusCode.BadRequest, 
                                $"Email '{model.Email}' đã tồn tại", "Hãy thử lại email khác");
                    default:
                        Console.WriteLine($"Error: {err.Code}\n- Detail: {err.Description}");
                        throw new AppException(HttpStatusCode.BadRequest, 
                            "Đăng kí không thành công", "Hãy thử lại");
                }
            }
        }
        await _userManager.AddToRoleAsync(user, erole.ToString());
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = new SuccessSignupModel()
            {
                AccessToken = GenerateJwtToken(user, erole),
                // RefreshToken = GenerateRefreshToken(user),
            }
        };
    }


    private string GenerateJwtToken(User User, ERole role)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, User.Id),
            new Claim(ClaimTypes.Name, User.UserName!),
            new Claim(ClaimTypes.Role, role.ToString())
        });
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"]!,
            null,
            identity.Claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // private string GenerateRefreshToken(User User)
    // {
    //     return _userManager.GenerateUserTokenAsync(User, "Default", "refresh_token").Result;
    // }
}