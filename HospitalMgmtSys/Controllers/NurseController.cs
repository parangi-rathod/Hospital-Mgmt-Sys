using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Service;

namespace HospitalMgmtSys.Controllers
{
    [Authorize("Nurse")]
    public class NurseController : BaseController
    {
        #region props

        private readonly INurseService _nurseService;
        

        #endregion

        #region ctor
        public NurseController(INurseService nurseService)
        {
            _nurseService= nurseService;
        }
        #endregion

        [HttpGet("NurseDuties")]
        public async Task<IActionResult> CheckDuties()
        {
            var nurseId = User.Claims.FirstOrDefault(u => u.Type == "Id");
            int idUser = 0;

            if (nurseId != null && int.TryParse(nurseId.Value, out int id))
            {
                idUser = id;
            }
            var response = await _nurseService.checkDuties();
            return Ok(response);
        }
    }
}
