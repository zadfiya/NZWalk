using Microsoft.AspNetCore.Identity;

namespace NZWalk.API.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles );
    }
}
