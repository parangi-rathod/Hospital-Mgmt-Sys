using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Repository.Model;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class PatientService : IPatientService
    {
        #region properties        
        private readonly IPatientRepo _patientRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region ctor
        public PatientService(IPatientRepo patientRepo, IHttpContextAccessor httpContextAccessor) {

            _patientRepo = patientRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region current appointments
        public async Task<ResponseDTO> getCurrentAppointment()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");
                int patientId = 0;
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int res))
                {
                    patientId = res;
                }
                DateTime currTime = DateTime.Now;
                // Await the asynchronous method to get the actual Appointment object
                Appointment currentAppo = await _patientRepo.GetCurrentAppointment(patientId, currTime);

                if (currentAppo == null)
                {
                    return new ResponseDTO { Status = 400, Message = "No current appointment" };
                }

                return new ResponseDTO { Status = 200, Data = currentAppo, Message = "Current appointment" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 400, Message = ex.Message };
            }
        }
        #endregion
        
        #region appointment history
       
        public async Task<ResponseDTO> appointmentHistory()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");
                int patientId = 0;
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int res))
                {
                    patientId = res;
                }
                List<dynamic> appoHist = await _patientRepo.AppointmentHistory(patientId);
                if (appoHist == null)
                {
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "No Previous appointments"
                    };
                }
                return new ResponseDTO
                {
                    Status = 200,
                    Data = appoHist,
                    Message = "Previous appointments"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 500, Message = ex.Message };
            }
        }
        #endregion
    }
}
