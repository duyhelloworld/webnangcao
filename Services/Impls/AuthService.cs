using webnangcao.Models.Securities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using webnangcao.Entities;
using webnangcao.Exceptions;
using webnangcao.Enumerables;
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

    public async Task<ResponseModel> SignInAsync(SigninModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Tài khoản không tồn tại", "Hãy đăng kí trước");
                
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
        if (!result.Succeeded)
        {
            Console.WriteLine("Error: " + result);
            return new ResponseModel()
            {
                IsSucceed = false,
                Data = "Tài khoản hoặc mật khẩu không chính xác"
            };
        }
        var highestRole = ERoleTool.GetHighestRole(await _userManager.GetRolesAsync(user));
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = GenerateJwtToken(user, highestRole)
        };
    }

    public async Task<ResponseModel> SignUpAsync(SignupModel model)
    {
        var user = new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                switch (err.Code)
                {
                    case "DuplicateUserName":
                        throw new AppException(HttpStatusCode.Conflict, 
                                $"Tên đăng nhập '{model.UserName}' đã tồn tại", "Hãy thử lại tên khác");
                    case "DuplicateEmail":
                        throw new AppException(HttpStatusCode.Conflict, 
                                $"Email '{model.Email}' đã tồn tại", "Hãy thử lại email khác");
                    default:
                        Console.WriteLine($"Error: {err.Description}");
                        throw new AppException(HttpStatusCode.BadRequest, 
                            "Đăng kí không thành công", "Hãy thử lại");
                }
            }
        }
        await _userManager.AddToRoleAsync(user, ERole.USER.ToString());
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = GenerateJwtToken(user, ERole.USER)
        };
    }

    private string GenerateJwtToken(User user, ERole erole)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new ClaimsIdentity(new[]
        {
            new Claim("userid", user.Id.ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Role, ERoleTool.ToString(erole)),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
        });
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtInfo:Key"]!)),
                SecurityAlgorithms.HmacSha512Signature);
        var expireTime = DateTime.UtcNow.AddDays(
            _config.GetSection("JwtInfo:ExpireDays").Get<int>());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = expireTime,
            SigningCredentials = credentials,
            Issuer = _config["JwtInfo:Issuer"]!,
        };
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public async Task<bool> ValidateToken(string token) 
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var result = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters()
        {
            ValidIssuer = _config["JwtInfo:Issuer"]!,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtInfo:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true
        });
        if (!result.IsValid)
        {
            return false;
        }
        return true;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ResponseModel> ChangePasswordAsync(long userId, ChangePasswordModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString()) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Tài khoản không tồn tại", "Hãy đăng kí trước");
        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                switch (err.Code)
                {
                    case "PasswordMismatch":
                        throw new AppException(HttpStatusCode.BadRequest, 
                                "Mật khẩu cũ không chính xác", "Hãy thử lại");
                    default:
                        Console.WriteLine($"Error: {err.Description}");
                        throw new AppException(HttpStatusCode.BadRequest, 
                            "Đổi mật khẩu không thành công", "Hãy thử lại");
                }
            }
        }
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = "Đổi mật khẩu thành công"
        };
    }
}