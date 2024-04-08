using AutoMapper;
using Repository.Interface;
using Repository.Model;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class ReceptionistService : IReceptionistService
    {
        #region properties
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IAuthRepo _authRepo;
        private readonly IReceptionistRepo _receptionistRepo;
        private readonly IUserRepo _userRepo;
        private readonly IDoctorRepo _docRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IPasswordHash _passwordHash;
        private readonly IValidationService _validationService;

        #endregion

        #region ctor
        public ReceptionistService(IAuthRepo authRepo, IMapper mapper, IEmailService emailService,IPatientRepo patientRepo, IReceptionistRepo receptionistRepo, IUserRepo userRepo, IDoctorRepo docRepo, IPasswordHash passwordHash, IValidationService validationService)
        {
            _mapper = mapper;
            _emailService = emailService;
            _authRepo = authRepo;
            _userRepo = userRepo;
            _docRepo = docRepo;
            _patientRepo= patientRepo;
            _receptionistRepo= receptionistRepo;
            _passwordHash = passwordHash;
            _validationService = validationService;
        }

        #endregion
        public async Task<ResponseDTO> RegisterPatient(RegisterPatientDTO registerPatientDTO)
        {
            try
            {
                var validationResult = await _validationService.ValidatePatient(registerPatientDTO);
                if (validationResult.IsValid)
                {
                    if (await _userRepo.UserExistsByEmail(registerPatientDTO.Email) )
                    {
                        return new ResponseDTO { Status = 400, Message = "Patient already exists." };
                    }

                    var patientPassword = $"{registerPatientDTO.FirstName}{registerPatientDTO.DateOfBirth:yyyyMMdd}";

                    string passHash = _passwordHash.GeneratePasswordHash(patientPassword);
                    var user = _mapper.Map<Users>(registerPatientDTO);
                    user.Password = passHash;

                    var reg = await _authRepo.Register(user);
                    string Fullname = registerPatientDTO.FirstName + " " + registerPatientDTO.LastName;
                    var emailDTO = new EmailDTO
                    {
                        Email = registerPatientDTO.Email,
                        Name = Fullname,
                        Subject = "Successfully Registered",
                        Body = "<html><body>" +
                           "<div style='border: 2px solid #000; padding: 20px;'>" +
                           "<h1 style='color: blue;'>Welcome to Sterling Hospitals!</h1>" +
                           "<p>You have successfully registered as " + user.Role + ".</p>" +
                           "<p>You can log in to the system using your password "+ patientPassword+ "</p>" +
                           "</div>" +
                           "</body></html>"
                    };

                    _emailService.SendEmail(emailDTO);
                    return new ResponseDTO { Status = 200, Message = "Patient registered successfully." };
                }
                else
                {
                    return new ResponseDTO { Status = 400, Message = "Validation failed.", Error = string.Join("; ", validationResult.Errors) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Error = ex.Message };
            }
        }

        public async Task<ResponseDTO> ScheduleAppointment(AppointmentDTO appointmentDTO)
        {
            try
            {
                var validationResult = await _validationService.ValidateAppointment(appointmentDTO);
                if (!validationResult.IsValid)
                {
                    return new ResponseDTO { Status = 400, Message = "Validation failed.", Error = string.Join("; ", validationResult.Errors) };
                }

                var user = await _userRepo.UserById(appointmentDTO.PatientId);
                //if (user != null)
                //{
                //    return new ResponseDTO { Status = 400, Message = "Patient does not exist; please register first." };
                //}

                var patient = await _patientRepo.IsPatientExists(user.FirstName, user.DateOfBirth, user.Email);
                if (patient == null && user==null)
                {
                    return new ResponseDTO { Status = 400, Message = "Patient does not exist; please register first." };
                }

                var doctor = await _docRepo.DoctorBySpecialization(appointmentDTO.ConsultDoctor);

                appointmentDTO.ScheduleStartTime = DateTime.Now;
                appointmentDTO.ScheduleEndTime = DateTime.Now.AddHours(3);

                var isDoctorAvail = await _docRepo.checkAvailability(appointmentDTO.ConsultDoctor, appointmentDTO.ScheduleStartTime);
                if (!isDoctorAvail)
                {
                    return new ResponseDTO { Status = 400, Message = "Doctor is not available at given time" };
                }

                var patientAppo = _mapper.Map<Appointment>(appointmentDTO);
                await _receptionistRepo.ScheduleAppointment(patientAppo);

                var emailDTO = new EmailDTO
                {
                    Email = patient.Email,
                    Subject = "Appointment scheduled",
                    Body = "<html><body>" +
                   "<div style='border: 2px solid #000; padding: 20px;'>" +
                   "<h1 style='color: blue;'>Welcome to Sterling Hospitals!</h1>" +
                   "<p style='font-size: 16px;'>Dear " + patient.FirstName + " " + patient.LastName + ",</p>" +
                   "<p style='font-size: 16px;'>Your appointment has been scheduled with Dr. " + doctor.FirstName + " " + doctor.LastName + " (" + appointmentDTO.ConsultDoctor + ").</p>" +
                   "<p style='font-size: 16px;'>Appointment Details:</p>" +
                   "<ul>" +
                   "<li><strong>Schedule Start Time:</strong> " + appointmentDTO.ScheduleStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "</li>" +
                   "<li><strong>Schedule End Time:</strong> " + appointmentDTO.ScheduleEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "</li>" +
                   "</ul>" +
                   "</div>" +
                   "</body></html>"
    
                };

                _emailService.SendEmail(emailDTO);
                return new ResponseDTO { Status = 200, Message = "Appointment scheduled successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Error = ex.Message };
            }
        }
    }
}
