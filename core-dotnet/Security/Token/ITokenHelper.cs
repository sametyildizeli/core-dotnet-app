using core_dotnet.Entities;
using core_dotnet.Security.Models;

namespace core_dotnet.Security.Token;

public interface ITokenHelper
{
    AccessToken CreateToken(IBaseUserEntity user, IEnumerable<string> userRoles);
}
