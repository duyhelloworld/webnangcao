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

namespace webnangcao.Services.Impl;

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

        var highestRole = (await _userManager.GetRolesAsync(user))
            .Select(r => ERoleTool.ToERole(r)).Max();
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
        var User = new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            FullName = model.FullName,
            Address = model.Address
        };

        var result = await _userManager.CreateAsync(User, model.Password);
        if (!result.Succeeded)
        {
            throw new AppException(HttpStatusCode.BadRequest, "Đăng kí không thành công", "Hãy thử lại");
        }
        var erole = ERoleTool.ToERole(role);
        await _userManager.AddToRoleAsync(User, erole.ToString());
        var refreshToken = await _userManager.GenerateUserTokenAsync(User, "Default", "refresh_token");
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = new SuccessSignupModel()
            {
                AccessToken = GenerateJwtToken(User, erole),
                RefreshToken = refreshToken
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
            _config["Jwt:Issuer"] ?? "",
            _config["Jwt:Audience"] ?? "",
            identity.Claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}