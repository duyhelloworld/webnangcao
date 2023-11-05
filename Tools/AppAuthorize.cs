using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using webnangcao.Entities.Enumerables;

namespace webnangcao.Tools;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AppAuthorize : AuthorizeAttribute
{
    public AppAuthorize(params ERole[] eroles)
    {
        Roles = string.Join(", ", eroles.Select(r => ERoleTool.ToString(r)));
        AuthenticationSchemes = @$"
                {JwtBearerDefaults.AuthenticationScheme}
                ";
                // {FacebookDefaults.AuthenticationScheme}
                // {GoogleDefaults.AuthenticationScheme},
    }
}