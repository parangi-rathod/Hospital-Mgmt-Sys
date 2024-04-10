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
       

        #endregion

        #region ctor
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        #endregion

        [HttpGet("Dashboard/CurrentAppointment")]
        public async Task<IActionResult> GetCurrentAppointment()
        {           
            var response = await _patientService.getCurrentAppointment();
            return Ok(response);
        }
        [HttpGet("Dashboard/AppointmentHistory")]
        public async Task<IActionResult> AppointmentHistory()
        {           
            var response = await _patientService.appointmentHistory();
            return Ok(response);
        }
    }
}
