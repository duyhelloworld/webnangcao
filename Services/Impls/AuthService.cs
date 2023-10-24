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

namespace webnangcao.Services.Impl;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
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

        var highestRole = (await _userManager.GetRolesAsync(user))
            .Select(r => ERoleConverter.ToERole(r)).Max();
        var refreshToken = await _userManager.GenerateUserTokenAsync(user, "Default", "refresh_token");
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = new SuccessSignupModel()
            {
                UserId = user.Id,
                AccessToken = GenerateJwtToken(user, highestRole),
                RefreshToken = refreshToken
            }
        };
    }

    public async Task<ResponseModel> SignUpAsync(SignupModel model, string role)
    {
        var appuser = new AppUser()
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            FullName = model.FullName,
            Address = model.Address
        };

        var result = await _userManager.CreateAsync(appuser, model.Password);
        if (!result.Succeeded)
        {
            throw new AppException(HttpStatusCode.BadRequest, "Đăng kí không thành công", "Hãy thử lại");
        }
        var erole = ERoleConverter.ToERole(role);
        await _userManager.AddToRoleAsync(appuser, erole.ToString());
        var refreshToken = await _userManager.GenerateUserTokenAsync(appuser, "Default", "refresh_token");
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = new SuccessSignupModel()
            {
                AccessToken = GenerateJwtToken(appuser, erole),
                RefreshToken = refreshToken
            }
        };
    }

    private string GenerateJwtToken(AppUser appUser, ERole role)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Name, appUser.UserName!),
            new Claim(ClaimTypes.Role, role.ToString())
        });
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"] ?? "",
            _config["Jwt:Audience"] ?? "",
            identity.Claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}