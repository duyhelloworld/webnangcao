using webnangcao.Models.Securities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using webnangcao.Entities;
using webnangcao.Exceptions;

namespace webnangcao.Services.Impl;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task<ResponseModel> SigninAsync(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName) 
            ?? throw new AppException(HttpStatusCode.NotFound, "Tài khoản không tồn tại", "Hãy đăng kí trước");
        if (await _userManager.CheckPasswordAsync(user, model.Password))
        {
            throw new AppException(HttpStatusCode.BadRequest, "Tài khoản hoặc mật khẩu không chính xác", "Hãy thử lại");
        }
        await _signInManager.SignInAsync(user, model.RememberMe);
        return new ResponseModel()
        {
            IsSucceed = true,
            Data = GenerateJwtToken(user)
        };

        // Hoặc đơn giản logic
        // var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
        // if (result.Succeeded)
        // {
        //     return new ResponseModel()
        //     {
        //         IsSucceed = true,
        //         Data = GenerateJwtToken((await _userManager.FindByNameAsync(model.UserName))!)
        //     };
        // }
    }

    public async Task Signup(SignupModel model)
    {
        var user = new AppUser()
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new AppException(HttpStatusCode.BadRequest, "Đăng kí không thành công", "Hãy thử lại");
        }
    }

    private string GenerateJwtToken(AppUser appUser)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Name, appUser.UserName!)
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