using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Service
{
    public class JWTTokenService : IJWTTokenService
    {
        #region props
        private readonly IConfiguration _config;
        #endregion

        #region ctor
        public JWTTokenService(IConfiguration configuration)
        {
            _config = configuration;
        }
        #endregion

        public string GenerateJwtToken(string userRole, string userId)
        {
            var uId = userId.ToString();
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", uId),
                new Claim(ClaimTypes.Role, userRole),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred
            );
            var jwt =  new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public bool Validate(string jwtToken, out JwtSecurityToken jwtSecurityToken, out string nameIdentifier)
        {
            jwtSecurityToken = null;
            nameIdentifier = null;

            if (jwtToken == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;

                if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
                    return false;

                nameIdentifier = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                return jwtSecurityToken != null;
            }
            catch
            {
                return false;
            }
        }

    }
}
