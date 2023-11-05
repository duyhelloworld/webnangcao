using webnangcao.Entities.Enumerables;
using webnangcao.Models.Securities;

namespace webnangcao.Services;

public interface IAuthService
{
    Task<ResponseModel> SignInAsync(SigninModel model);

    // Signup mặc định luôn là User
    Task<ResponseModel> SignUpAsync(SignupModel model);

    Task<bool> ValidateToken(string token);

    Task SignOutAsync();
}