using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Repository.Model;
using Repository.Repository;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class DoctorService : IDoctorService
    {
        #region properties
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IAuthRepo _authRepo;
        private readonly IReceptionistRepo _receptionistRepo;
        private readonly IUserRepo _userRepo;
        private readonly IDoctorRepo _docRepo;
        private readonly INurseRepo _nurseRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IPasswordHash _passwordHash;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpCon;
        private readonly IJWTTokenService _jwtToken;

        #endregion

        #region ctor
        public DoctorService(IAuthRepo authRepo, INurseRepo nurseRepo, IJWTTokenService jwtToken, IMapper mapper, IEmailService emailService, IPatientRepo patientRepo, IReceptionistRepo receptionistRepo, IUserRepo userRepo, IDoctorRepo docRepo, IPasswordHash passwordHash, IValidationService validationService)
        {
            _mapper = mapper;
            _emailService = emailService;
            _authRepo = authRepo;
            _userRepo = userRepo;
            _docRepo = docRepo;
            _nurseRepo = nurseRepo;
            _patientRepo = patientRepo;
            _receptionistRepo = receptionistRepo;
            _passwordHash = passwordHash;
            _validationService = validationService;
        }



        #endregion
        public async Task<List<dynamic>> getDoctorAppointments(int doctorId)
        {
            try
            {
                var doctorAppointments = await _docRepo.checkAppointments(doctorId);
                //no appointments also
                return doctorAppointments;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public async Task<ResponseDTO> rescheduleAppointment(RescheduleAppoDTO rescheduleAppoDTO, int doctorId)
        {
            try
            {
                var isAppoExists = await _docRepo.isAppointmentExists(rescheduleAppoDTO.AppointmentId, doctorId);
                if (!isAppoExists)
                {
                    return new ResponseDTO { Status = 400, Message = "Invalid appointment id" };
                }
                var info = await _docRepo.rescheduleAppointment(rescheduleAppoDTO.AppointmentId, rescheduleAppoDTO.NewStartTime, rescheduleAppoDTO.NewEndTime);
                //string fullName = info.PatientFirstName + info.PatientLastName;
                var emailDTO = new EmailDTO
                {
                    Email = info,
                    Subject = "Appointment Rescheduled",
                    Body = "<html><body>" +
                           "<div style='border: 2px solid #000; padding: 20px;'>" +
                           "<h1 style='color: blue;'>Appointment Rescheduled</h1>" +
                           "<p>Dear Patient," +
                           "<p>Your appointment has been successfully re-scheduled.</p>" +
                           "<p>Please go and check on your dashboard" +
                           //"<p>New Schedule:</p>" +
                           //"<p>Start Time: " + info.ScheduleStartTime.ToString("yyyy-MM-dd HH:mm") + "</p>" +
                           //"<p>End Time: " + info.ScheduleEndTime.ToString("yyyy-MM-dd HH:mm") + "</p>" +
                           "</div>" +
                           "</body></html>"
                };
                _emailService.SendEmail(emailDTO);

                return new ResponseDTO { Status = 200, Message = "Appointment rescheduled successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 400, Message = ex.Message };
            }
        }

        public async Task<ResponseDTO> cancelAppointment(int appointmentId, int doctorId)
        {
            try
            {
                var isAppoExists = await _docRepo.isAppointmentExists(appointmentId, doctorId);
                if (!isAppoExists)
                {
                    return new ResponseDTO { Status = 400, Message = "Invalid appointment id" };
                }
                var cancelAppo = await _docRepo.cancelAppointment(appointmentId);
                var email = cancelAppo;
                var emailDTO = new EmailDTO
                {
                    Email = email,
                    Subject = "Appointment Rescheduled",
                    Body = "<html><body>" + "<div style='border: 2px solid #000; padding: 20px;'>" + "<h1 style='color: blue;'>Appointment Rescheduled</h1>" + "<p>Dear user," + "<p>Your appointment has been successfully cancelled.</p>" +
                   "</body></html>"
                };

                return new ResponseDTO { Status = 200, Message = "Appointment cancelled successfully" };
            }
            catch (Exception ex) { return new ResponseDTO { Status = 400, Message = ex.Message }; }
        }
        public async Task<ResponseDTO> assignNurse(AssignNurseDTO assignNurseDTO, int doctorId)
        {
            try
            {
                var isAppoExists = await _docRepo.isAppointmentExists(assignNurseDTO.AppointmentId, doctorId);
                if (!isAppoExists)
                {
                    return new ResponseDTO { Status = 400, Message = "Invalid appointment id" };
                }
                var isNurseExists = await _nurseRepo.isNurseExists(assignNurseDTO.NurseID);
                if (!isNurseExists)
                {
                    return new ResponseDTO { Status = 400, Message = "Nurse doesn't exists" };
                }
                var assignNurse = await _docRepo.assignNurse(assignNurseDTO.NurseID, assignNurseDTO.AppointmentId);
                return new ResponseDTO { Status = 200, Message = "Nurse assigned successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 400, Message = ex.Message };
            }


        }
    }
}
