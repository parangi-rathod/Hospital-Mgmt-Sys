using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class NurseService : INurseService
    {
        #region properties
        private readonly INurseRepo _nurseRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region ctor
        public NurseService(IHttpContextAccessor httpContextAccessor,INurseRepo nurseRepo)
        {            
            _nurseRepo = nurseRepo;            
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region check duties
        public async Task<ResponseDTO> checkDuties()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");
                int nurse = 0;
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int res))
                {
                    nurse = res;
                }
                var nurseAppointments = await _nurseRepo.nurseDuties(nurse);
                if (nurseAppointments == null)
                {
                    return new ResponseDTO { Status = 200, Message = "No current nurse duties assigned yet" };
                }
                return new ResponseDTO { Status = 200, Data = nurseAppointments, Message = "Current nurse duties" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 400, Message = ex.Message };
            }
        }
        #endregion
    }
}
