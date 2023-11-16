using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;

namespace webnangcao.Services;

public interface IPlaylistService
{
    // Guest
    //  Lấy playlist có id = playlistId nếu được đặt public
    Task<PlaylistResponseModel?> GetPublicById(int playlistId);
    // Lấy random trong các playlist public
    Task<IEnumerable<PlaylistResponseModel>> GetRandom();

    // Admin
    // Lấy tất cả playlist của tất cả user
    Task<IEnumerable<PlaylistResponseModel>> GetAll();

    // User
    // Lấy tất cả playlist của user đăng nhập
    Task<IEnumerable<PlaylistResponseModel>> GetAllByUser(long userId);
    // Lấy playlist có id = playlistId của user đăng nhập
    Task<PlaylistResponseModel?> GetOfUserById(int playlistId, long userId);

    // Like/Dislike 1 playlist bất kì bằng tài khoản của user đăng nhập
    Task Like(int playlistId, long userId);

    // User tạo mới 1 playlist
    Task<int> AddNew(string jsonModel, IFormFile? artwork, long userId);
    // User repost 1 playlist
    Task Repost(int playlistId, long userId);
    // User cập nhật thông tin 1 playlist (cả danh sách bài hát trong đó)
    Task UpdateInfomation(PlaylistUpdateModel model, IFormFile? artwork, long userId);
    // User xóa 1 playlist
    Task Delete(int playlistId, long userId);
}