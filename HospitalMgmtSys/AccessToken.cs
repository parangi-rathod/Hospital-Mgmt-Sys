using System.Security.Claims;

namespace HospitalMgmtSys
{
    public class AccessToken 
    {
        public int GetDoctorIdFromClaims(ClaimsPrincipal User)
        {
            var doctorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (doctorIdClaim != null && int.TryParse(doctorIdClaim.Value, out int doctorId))
            {
                return doctorId;
            }
            return 0; 
        }
    }
}
