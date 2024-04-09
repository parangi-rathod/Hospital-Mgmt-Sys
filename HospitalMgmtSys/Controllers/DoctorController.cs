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
            var response = await _doctorService.GetDoctorAppointments(idUser);
            return Ok(response);
        }
    }
}
