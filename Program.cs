using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Entities.Enumerables;
using webnangcao.Exceptions;
using webnangcao.Services;
using webnangcao.Services.Impl;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddScoped<ErrorMiddleware>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOPT => 
{
    jwtOPT.RequireHttpsMetadata = false;
    jwtOPT.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidIssuer = config["Authentication:Jwt:Issuer"],
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Authentication:Jwt:Key"]!)),
    };
})
.AddFacebook(options =>
{
    options.AppId = config["Authentication:Facebook:AppId"]!;
    options.AppSecret = config["Authentication:Facebook:AppSecret"]!;
})
.AddGoogle(Options => 
{
    Options.ClientId = config["Authentication:Google:ClientId"]!;
    Options.ClientSecret = config["Authentication:Google:ClientSecret"]!;
});

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;

    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 0;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
    options.Stores.MaxLengthForKeys = 128;
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// } 
// else
// {
//     app.UseMiddleware<ErrorMiddleware>();
// }

app.UseMiddleware<ErrorMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();