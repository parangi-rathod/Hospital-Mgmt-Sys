using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace HospitalMgmtSys.Controllers
{
    public class AuthController : BaseController
    {
        #region props

        private readonly IAuthService _authService;

        #endregion

        #region ctor
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region APIs

        [HttpPost]
        [Route("RegisterUser")]
        [Authorize("Doctor")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerDTO)
        {
            var response = await _authService.RegisterUser(registerDTO);
            return StatusCode(response.Status, response);
        }
        [HttpPost]
        [Route("RegisterDoctor")]
        [Authorize("Doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorDTO registerDTO)
        {
            var response = await _authService.RegisterDoctor(registerDTO);
            return StatusCode(response.Status, response);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var response = await _authService.Login(loginDTO);
            return StatusCode(response.Status, response);
        }

        #endregion
    }
}
