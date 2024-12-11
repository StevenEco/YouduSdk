using YouduSdk.Entity;
using YouduSdk.Entity.Entities;

namespace YouduSdk.Core.Interfaces;

public interface ITokenService
{
    Task<YdToken> GetToken(YdRequest param);
    Task<YdToken> CheckAndRefreshToken(YdRequest param);
}
