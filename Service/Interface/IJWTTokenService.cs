using System.IdentityModel.Tokens.Jwt;

namespace Service.Interface
{
    public interface IJWTTokenService
    {
        string GenerateJwtToken(string userId, string userRole);
        bool Validate(string jwtToken, out JwtSecurityToken jwtSecurityToken, out string nameIdentifier)
    }
}
