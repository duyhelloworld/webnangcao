using System.Net;
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Exceptions;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;
using webnangcao.Tools;

namespace webnangcao.Services.Impl;

public class UserService : IUserService
{
    private readonly int PAGE_SIZE = 10;
    private readonly ApplicationContext _dbContext;
    public UserService(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserResponseModel>> GetAll(int page)
    {
        return await _dbContext.Users
            .Skip((page - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .Select(u => new UserResponseModel
            {
                Id = u.Id,
                UserName = u.UserName!,
                Email = u.Email!,
                Avatar = u.Avatar,
                FullName = UserTool.GetAuthorName(u),
                PhoneNumber = u.PhoneNumber!
            })
            .ToListAsync();
    }

    public async Task<UserResponseModel> GetById(long uid)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == uid);
        if (user == null)
        {
            throw new AppException(HttpStatusCode.NotFound, "Không tồn tại user này");
        }
        return new UserResponseModel() 
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Avatar = user.Avatar,
            FullName = UserTool.GetAuthorName(user),
            PhoneNumber = user.PhoneNumber!
        };
    }

    public async Task<bool> Update(long uid, UserUpdateModel model, IFormFile? avatar)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == uid);
        if (user == null)
        {
            throw new AppException(HttpStatusCode.NotFound, "Không tồn tại user này");
        }
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        if (avatar != null)
        {
            user.Avatar = await FileTool.(avatar, "users");
        }
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public Task Disable(long uid)
    {

    }
}