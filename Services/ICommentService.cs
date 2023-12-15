using webnangcao.Entities;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;
namespace webnangcao.Services;

public interface ICommentService
{
    //Guest
    //Xem tất cả comment của 1 track
    Task<IEnumerable<CommentResponseModel>> GetByTrackId(int trackId);

    //Admin
    //Xem tất cả comment ủa app (để kiểm tra xem có comment vi phạm hay không)
    Task<IEnumerable<CommentResponseModel>> GetAll();
    //Xem các bình luận vi phạm
    Task<IEnumerable<CommentResponseModel>> GetViolationComment();
    //Xóa comment của người dùng bởi admin
    Task DeleteCommentByAdmin(int commentId);
    //Bỏ report comment
    Task UnReportComment(int commentId);

    //Member
    //Báo cáo vi phạm comment
    Task ReportComment(int commentId, long userid);
    //Xóa comment của mình
    Task DeleteCommentByCreator(int commentId, long userid);
    //Sửa comment của mình
    Task UpdateCommentByCreator(int commentId, long userid, CommentUpdateModel model);
    //Thêm comment vào track
    Task Comment(CommentInsertModel model, long userid, int trackId);
}