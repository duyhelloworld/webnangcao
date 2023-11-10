using Microsoft.AspNetCore.Mvc;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;

namespace webnangcao.Services;

public interface IPlaylistService
{
    // Guest
    // - Nên để tích AI rồi generate playlist cho guest
    Task<IEnumerable<PlaylistResponseModel>> GetAllPublic();
    Task<PlaylistResponseModel?> GetPublicById(int playlistId);
    Task Play(int playlistId);


    // User
    Task<IEnumerable<PlaylistResponseModel>> Search(string keyword, long? userId);
    Task<IEnumerable<PlaylistResponseModel>> GetAllByUser(long userId);
    Task<PlaylistResponseModel?> GetOfUserById(int playlistId, long userId);

    Task Like(int playlistId, long userId);
    Task<int> AddNew(PlaylistInsertModel model, long userId);
    Task SaveToLibrary(int playlistId, long userId);
    Task UpdateInfomation(PlaylistUpdateModel model, int playlistId, long userId);
    Task Delete(int playlistId, long userId);

    // Admin
    Task<IEnumerable<PlaylistResponseModel>> GetAll();
}