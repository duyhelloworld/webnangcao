using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Securities;
using webnangcao.Models.Updates;

namespace webnangcao.Services;

public interface IPlaylistService
{
    // Guest
    // Lấy theo phân trang các playlist public
    Task<IEnumerable<PlaylistResponseModel>> GetAllPublic(int page);
    //  Lấy playlist có id = playlistId nếu được đặt public
    Task<PlaylistResponseModel?> GetPublicById(int playlistId);
    // Tìm kiếm theo tên playlist/tên người tạo/mô tả
    Task<IEnumerable<PlaylistResponseModel>> Search(string keyword);

    // Admin
    // Lấy tất cả playlist của tất cả user
    Task<IEnumerable<PlaylistResponseModel>> GetAllPublicAndPrivate();


    // User
    // Lấy tất cả playlist của user đăng nhập
    Task<IEnumerable<PlaylistResponseModel>> GetAllPlaylistCreatedByUser(long userId);
    // Lấy playlist trong danh sách playlist của user đăng nhập
    Task<PlaylistResponseModel?> GetByIdInUserCreatedPlaylist(int playlistId, long userId);

    // Like/Dislike 1 playlist bất kì bằng tài khoản của user đăng nhập
    Task Like(int playlistId, long userId);

    // User tạo mới 1 playlist. trả về thông báo + mã tạo mới
    Task<ResponseModel> AddNew(PlaylistInsertModel model, IFormFile? artwork, long userId);

    // User repost 1 playlist
    Task Repost(int playlistId, long userId);

    // User cập nhật thông tin 1 playlist (cả danh sách bài hát trong đó)
    Task UpdateInfomation(int playlistId, PlaylistUpdateModel model, IFormFile? artwork, long userId);
    
    // User xóa 1 playlist
    Task DeleteByCreator(int playlistId, long userId);
    Task DeleteByAdmin(int playlistId);
}