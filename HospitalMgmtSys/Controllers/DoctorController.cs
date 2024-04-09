using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;
using Service.Service;

namespace HospitalMgmtSys.Controllers
{
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

        [HttpGet("Dashboard")]
        public async Task<IActionResult> CheckAppointment()
        {
            var response = await _doctorService.GetDoctorAppointments();
            return Ok(response);
        }
    }
}
