using System.Security.Claims;

namespace HospitalMgmtSys
{
        public interface IAccToken
        {
            int GetDoctorIdFromClaims(ClaimsPrincipal user);
        }
}
