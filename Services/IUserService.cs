using webnangcao.Models.Responses;
using webnangcao.Models.Updates;

namespace webnangcao.Services;

public interface IUserService
{
    // Admin
    // Lấy tất cả user theo phân trang
    Task<List<UserResponseModel>> GetAll(int page);
    // Lấy user theo id
    Task<UserResponseModel> GetById(long uid);

    // Lấy avatar của user
    Task<Stream> GetAvatar(string fileName);
    
    // Xóa user
    Task Disable(long uid);
    
    // User
    // sửa thông tin user
    Task Update(long uid, UserUpdateModel model, IFormFile? avatar);

    // Đổi mật khẩu ở bên AuthService
}