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
        if (webnangcao != null && webnangcao.PasswordHash == null)
        {
            await _userManager.AddPasswordAsync(webnangcao, "password");
        }

        var superwebnangcao = await _userManager.FindByNameAsync("superwebnangcao");
        if (superwebnangcao != null && superwebnangcao.PasswordHash == null)
        {
            await _userManager.AddPasswordAsync(superwebnangcao, "password");
        }

        var duy = await _userManager.FindByNameAsync("duy");
        if (duy != null && duy.PasswordHash == null)
        {
            await _userManager.AddPasswordAsync(duy, "password");
        }

        var hiep = await _userManager.FindByNameAsync("hiep");
        if (hiep != null && hiep.PasswordHash == null)
        {
            await _userManager.AddPasswordAsync(hiep, "password");
        }

        var quang = await _userManager.FindByNameAsync("quang");
        if (quang != null && quang.PasswordHash == null)
        {
            await _userManager.AddPasswordAsync(quang, "password");
        }

        var chien = await _userManager.FindByNameAsync("chien");
        if (chien != null && chien.PasswordHash == null)
        {
            await _userManager.AddPasswordAsync(chien, "password");
        }

        var youzo = await _userManager.FindByNameAsync("youzo");
        if (youzo != null && youzo.PasswordHash == null)
        {
            await _userManager.AddPasswordAsync(youzo, "password");
        }
    }
}