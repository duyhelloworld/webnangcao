using System.Net;
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Entities.Joins;
using webnangcao.Exceptions;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;
using webnangcao.Tools;

namespace webnangcao.Services.Impls;

public class CommentService : ICommentService
{
    private readonly ApplicationContext _context;

    public CommentService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task DeleteCommentByAdmin(int commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy comment này",
                "Hãy thử lại");
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentByCreator(int commentId, long userid)
    {
        var comment = await _context.Comments.FindAsync(commentId)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy comment này",
                "Hãy thử lại");
        if (comment.UserId != userid)
        {
            throw new AppException(HttpStatusCode.Forbidden,
                "Bạn không có quyền xóa comment này",
                "Hãy thử lại");
        }
        _context.Comments.Remove(comment);
        comment.Track.CommentCount--;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentResponseModel>> GetAll()
    {
        var result = from comment in _context.Comments
                     join user in _context.Users
                         on comment.UserId equals user.Id
                     join track in _context.Tracks
                         on comment.TrackId equals track.Id
                     select new CommentResponseModel
                     {
                         Id = comment.Id,
                         Content = comment.Content,
                         CreatedAt = comment.CommentAt,
                         IsEdited = comment.IsEdited,
                         UserId = comment.UserId,
                         TrackId = comment.TrackId,
                         User = $"{user.FirstName} {user.LastName}"
                     };
        return await result.ToListAsync();
    }

    public async Task<IEnumerable<CommentResponseModel>> GetByTrackId(int trackId)
    {
        var result = from comment in _context.Comments
                     join user in _context.Users
                         on comment.UserId equals user.Id
                     join track in _context.Tracks
                         on comment.TrackId equals track.Id
                     where track.Id == trackId && !comment.IsReported
                     select new CommentResponseModel
                     {
                         Id = comment.Id,
                         Content = comment.Content,
                         CreatedAt = comment.CommentAt,
                         IsEdited = comment.IsEdited,
                         UserId = comment.UserId,
                         TrackId = comment.TrackId,
                         User = $"{user.FirstName} {user.LastName}"
                     };
        return await result.ToListAsync();
    }

    public async Task<IEnumerable<CommentResponseModel>> GetViolationComment()
    {
        var result = from comment in _context.Comments
                     join user in _context.Users
                         on comment.UserId equals user.Id
                     join track in _context.Tracks
                         on comment.TrackId equals track.Id
                     where comment.IsReported
                     select new CommentResponseModel
                     {
                         Id = comment.Id,
                         Content = comment.Content,
                         CreatedAt = comment.CommentAt,
                         IsEdited = comment.IsEdited,
                         UserId = comment.UserId,
                         TrackId = comment.TrackId,
                         User = $"{user.FirstName} {user.LastName}"
                     };
        return await result.ToListAsync();
    }

    public async Task ReportComment(int commentId, long userid)
    {
        var comment = await _context.Comments.FindAsync(commentId)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy comment này",
                "Hãy thử lại");
        if (comment.UserId == userid)
        {
            throw new AppException(HttpStatusCode.Forbidden,
                "Bạn không thể report comment của chính mình",
                "Hãy thử lại");
        }
        comment.IsReported = true;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCommentByCreator(int commentId, long userid, CommentUpdateModel model)
    {
        var comment = await _context.Comments.FindAsync(commentId)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy comment này",
                "Hãy thử lại");
        if (comment.UserId != userid)
        {
            throw new AppException(HttpStatusCode.Forbidden,
                "Bạn không có quyền chỉnh sửa comment này",
                "Hãy thử lại");
        }

        comment.Content = model.Content;
        comment.IsEdited = true;
        await _context.SaveChangesAsync();
    }
    public async Task Comment(CommentInsertModel model, long userid)
    {
        var track = await _context.Tracks.FindAsync(model.TrackId)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy bài hát này",
                "Hãy thử lại");
        var comment = new Comment()
        {
            Content = model.Content,
            CommentAt = DateTime.Now,
            TrackId = model.TrackId,
            UserId = userid,
        };
        await _context.Comments.AddAsync(comment);
        track.CommentCount++;
        await _context.SaveChangesAsync();
    }
}
