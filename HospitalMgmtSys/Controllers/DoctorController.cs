using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;
using Service.Service;
using System.Runtime.CompilerServices;

namespace HospitalMgmtSys.Controllers
{
    [Authorize("Doctor")]
    public class DoctorController : BaseController
    {
        #region props

        private readonly IDoctorService _doctorService;
        private readonly AccessToken _accToken;

        #endregion

        #region ctor
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        #endregion

       
        [HttpGet("Dashboard")]
        public async Task<IActionResult> CheckAppointment()
        {
            var doctorId = User.Claims.FirstOrDefault(u => u.Type == "Id");
            int idUser = 0;
            
            if (doctorId != null && int.TryParse(doctorId.Value, out int id))
            {
                idUser = id;   
            }
            var response = await _doctorService.getDoctorAppointments(idUser);
            return Ok(response);
        }

        [HttpPut("RescheduleAppointment")]
        public async Task<IActionResult> RescheduleAppointment(RescheduleAppoDTO rescheduleAppoDTO)
        {
            var doctorId = User.Claims.FirstOrDefault(u => u.Type == "Id");
            int idUser = 0;

            if (doctorId != null && int.TryParse(doctorId.Value, out int id))
            {
                idUser = id;
            }
            var response = await _doctorService.rescheduleAppointment(rescheduleAppoDTO, idUser);
            return Ok(response);
        }

        [HttpPut("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var doctorId = User.Claims.FirstOrDefault(u => u.Type == "Id");
            int idUser = 0;

            if (doctorId != null && int.TryParse(doctorId.Value, out int id))
            {
                idUser = id;
            }
            var response = await _doctorService.cancelAppointment(appointmentId, idUser);
            return Ok(response);
        }

        [HttpPut("AssignNurse")]
        public async Task<IActionResult> AssignNurse(AssignNurseDTO assignNurseDTO)
        {
            var doctorId = User.Claims.FirstOrDefault(u => u.Type == "Id");
            int idUser = 0;

            if (doctorId != null && int.TryParse(doctorId.Value, out int id))
            {
                idUser = id;
            }
            var response = await _doctorService.assignNurse(assignNurseDTO, idUser);
            return Ok(response);
        }


    }
}
