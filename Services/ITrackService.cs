using Microsoft.AspNetCore.Mvc;
using webnangcao.Entities;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;
namespace webnangcao.Services;

public interface ITrackService
{
    public Task<IEnumerable<TrackResponseModel>> GetAll();
    public Task<TrackResponseModel> GetById(int id);
    public Task<IEnumerable<TrackResponseModel>> GetByName(String name);
    // public Task<IEnumerable<TrackResponseModel>> Search(string input);
    public Task<IEnumerable<TrackResponseModel>> GetByUserId(int id);
    // public Task UploadTrack(TrackInsertModel model, long userId);
    public Task UpdateInfomation(TrackUpdateModel model, IFormFile? fileArtwork, int trackId);
    public Task UploadTrack(TrackInsertModel model, long userId, IFormFile fileAudio, IFormFile? fileArtwork);
    public Task Remove(int id);
    public Task LikeTrack(int userId, int trackId);
    // public Task PlayTrack(int id);
    public Task<IActionResult> PlayTrack(int trackId);
}