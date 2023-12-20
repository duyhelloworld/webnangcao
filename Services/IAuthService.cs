using webnangcao.Models.Securities;

namespace webnangcao.Services;

public interface IAuthService
{
    Task<ResponseModel> SignInAsync(SigninModel model);

    // Signup mặc định luôn là User
    Task<ResponseModel> SignUpAsync(SignupModel model);

    Task SignOutAsync();

    Task<ResponseModel> ChangePasswordAsync(long userId, ChangePasswordModel model);

    Task<AuthInformation?> ValidateToken(HttpRequest request);
    Task<AuthInformation?> ValidateToken(string token);
}