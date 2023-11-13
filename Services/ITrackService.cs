using webnangcao.Entities;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;

namespace webnangcao.Services;

public interface ITrackService
{
    public Task<IEnumerable<TrackResponseModel>> GetAll();
    public Task<IEnumerable<TrackResponseModel>> GetByName(String name);
    // public Task<IEnumerable<TrackResponseModel>> Search(string input);
    public Task<IEnumerable<TrackResponseModel>> GetByUserId(int id);
    public Task UploadTrack(TrackInsertModel model, long userId);
    public Task UpdateInfomation(TrackUpdateModel model, int id);
    public Task Remove(int id);
    public Task LikeTrack(int userId, int trackId);
    public Task CommentTrack(int id, int userId, string content);
    public Task<IEnumerable<Comment>> GetComment(int trackId);
    public Task EditComment(int id, string content);
    // public Task PlayTrack(int id);
    
}