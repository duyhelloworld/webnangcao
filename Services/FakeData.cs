using Microsoft.AspNetCore.Identity;
using webnangcao.Entities;

namespace webnangcao.Services;

public class FakeData
{
    private readonly UserManager<User> _userManager;
    public FakeData(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task InitDataAsync()
    {
        var webnangcao = await _userManager.FindByNameAsync("webnangcao");
        if (webnangcao != null && !await _userManager.HasPasswordAsync(webnangcao))
        {
            await _userManager.AddPasswordAsync(webnangcao, "password");
        }

        var superwebnangcao = await _userManager.FindByNameAsync("superwebnangcao");
        if (superwebnangcao != null && !await _userManager.HasPasswordAsync(superwebnangcao))
        {
            await _userManager.AddPasswordAsync(superwebnangcao, "password");
        }

        var duy = await _userManager.FindByNameAsync("duy");
        if (duy != null && !await _userManager.HasPasswordAsync(duy))
        {
            await _userManager.AddPasswordAsync(duy, "password");
        }

        var hiep = await _userManager.FindByNameAsync("hiep");
        if (hiep != null && !await _userManager.HasPasswordAsync(hiep))
        {
            await _userManager.AddPasswordAsync(hiep, "password");
        }
    }
}