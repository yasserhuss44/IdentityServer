using CustomIdentityServer4.Controllers;
using System;
using System.Threading.Tasks;

namespace CustomIdentityServer4.UserServices
{
    public interface ITokenService
    {
        Task<UserToken> GetTokenDetails(Guid tokenId);
        Task<UserToken> CreateToken(string userName);

        Task RemoveToken(Guid tokenId);
    }
}
