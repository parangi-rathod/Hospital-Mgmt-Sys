using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace HospitalMgmtSys.Controllers
{
    [Authorize("Receptionist")]
    public class ReceptionistController : BaseController
    {
        #region props

        private readonly IReceptionistService _receptionistService;

        #endregion

        #region ctor
        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }
        #endregion

        [HttpPost("RegisterPatient")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDTO registerDTO)
        {
            var response = await _receptionistService.RegisterPatient(registerDTO);
            return StatusCode(response.Status, response);
        }
        [HttpPost("ScheduleAppointment")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AppointmentDTO appointmentDTO)
        {
            var response = await _receptionistService.ScheduleAppointment(appointmentDTO);
            return StatusCode(response.Status, response);
        }
    }
}
