using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Service;

namespace HospitalMgmtSys.Controllers
{
    [Authorize("Patient")]
    public class PatientController : BaseController
    {
        #region props

        private readonly IPatientService _patientService;
        private readonly AccessToken _accToken;

        #endregion

        #region ctor
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        #endregion

        [HttpGet("Dashboard")]
        public async Task<IActionResult> GetCurrentAppointment()
        {
            var patientId = User.Claims.FirstOrDefault(u => u.Type == "Id");
            int idUser = 0;

            if (patientId != null && int.TryParse(patientId.Value, out int id))
            {
                idUser = id;
            }
            var response = await _patientService.getCurrentAppointment(idUser);
            return Ok(response);
        }
    }
}
