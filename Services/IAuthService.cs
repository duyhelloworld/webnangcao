using webnangcao.Models.Securities;

namespace webnangcao.Services;

public interface IAuthService
{
    public Task<ResponseModel> SigninAsync(LoginModel model);
    public Task Signup(SignupModel model);

}