using System.Security.Claims;

namespace HospitalMgmtSys
{
    public class AccessToken
    {
        public int GetUserIdFromClaims(ClaimsPrincipal user)
        {
            Claim userIdClaim = user.FindFirst("nameidentifier");

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new ArgumentException("Invalid or missing user ID in claims.");
            }

            return userId;
        }
    }
}
