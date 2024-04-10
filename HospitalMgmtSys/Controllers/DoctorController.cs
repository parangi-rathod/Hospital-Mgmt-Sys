using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;
using Service.Service;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace HospitalMgmtSys.Controllers
{
    [Authorize("Doctor")]
    public class DoctorController : BaseController
    {
        #region props

        private readonly IDoctorService _doctorService;
        
        #endregion

        #region ctor
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        #endregion

       
        [HttpGet("Dashboard/CheckAppointment")]
        public async Task<IActionResult> CheckAppointment()
        {                
            var response = await _doctorService.getDoctorAppointments();
            return Ok(response);
        }

        [HttpPut("RescheduleAppointment")]
        public async Task<IActionResult> RescheduleAppointment(RescheduleAppoDTO rescheduleAppoDTO)
        {
            var response = await _doctorService.rescheduleAppointment(rescheduleAppoDTO);
            return Ok(response);
        }

        [HttpPut("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var response = await _doctorService.cancelAppointment(appointmentId);
            return Ok(response);
        }

        [HttpPut("AssignNurse")]
        public async Task<IActionResult> AssignNurse(AssignNurseDTO assignNurseDTO)
        {
            var response = await _doctorService.assignNurse(assignNurseDTO);
            return Ok(response);
        }
      
        
    }
}
