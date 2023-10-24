using webnangcao.Entities.Enumerables;
using webnangcao.Models.Securities;

namespace webnangcao.Services;

public interface IAuthService
{
    public Task<ResponseModel> SignInAsync(LoginModel model);

    // Mặc định nếu signup không có role chỉ định thì luôn là User
    public Task<ResponseModel> SignUpAsync(SignupModel model, string role);

}