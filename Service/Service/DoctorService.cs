using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class DoctorService : IDoctorService
    {
        #region properties
        private readonly IEmailService _emailService;
        private readonly IDoctorRepo _docRepo;
        private readonly INurseRepo _nurseRepo;
        #endregion

        #region ctor
        public DoctorService(INurseRepo nurseRepo, IEmailService emailService, IDoctorRepo docRepo, IValidationService validationService)
        {
            _emailService = emailService;
            _docRepo = docRepo;
            _nurseRepo = nurseRepo;
        }
        #endregion

        public async Task<ResponseDTO> getDoctorAppointments(int doctorId)
        {
            try
            {
                var doctorAppointments = await _docRepo.checkAppointments(doctorId);
                if(doctorAppointments == null)
                {
                    return new ResponseDTO { Status = 200, Message = "No current doctor appointments" };
                }
                return new ResponseDTO { Status = 200, Data = doctorAppointments, Message = "Current doctor appointments" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 400, Message = ex.Message };
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
                // Check if the appointment exists and is assigned to the doctor
                var appointmentExists = await _docRepo.isAppointmentExists(assignNurseDTO.AppointmentId, doctorId);
                if (!appointmentExists)
                {
                    return new ResponseDTO { Status = 400, Message = "Invalid appointment id" };
                }

                // Check if the nurse exists
                var nurse = await _nurseRepo.isNurseExists(assignNurseDTO.NurseID);
                if (nurse == null)
                {
                    return new ResponseDTO { Status = 400, Message = "Nurse doesn't exist" };
                }

                // Assign nurse to appointment
                var result = await _docRepo.assignNurse(assignNurseDTO.NurseID, assignNurseDTO.AppointmentId);

                // Prepare email content
                //var emailBody = $"<html><body><div style='border: 2px solid #000; padding: 20px;'>" +
                //  $"<h1 style='color: blue;'>Hello, you have recently been assigned duty for the appointment:</h1>" +
                //  $"<p><strong>Patient Name:</strong> {result.firstname} {result.lastname}</p>" +
                //  $"<p><strong>Schedule Start Time:</strong> {result.scheduleStartTime}</p>" +
                //  $"<p><strong>Schedule End Time:</strong> {result.scheduleEndTime}</p>" +
                //  "</div></body></html>";

                var emailBody = "<html><body><div style='border: 2px solid #000; padding: 20px;'>" +
                                $"<h1 style='color: blue;'>Welcome to Streling Hospitals</h1>" + 
                                "</div>" + "<p>Hello, you have recently been assigned duty for the appointment, check on your dashboard</p>" + "</body></html>";

                // Prepare and send email
                var emailDTO = new EmailDTO
                {
                    Email = nurse,
                    Subject = "Assigned duty",
                    Body = emailBody
                };
                _emailService.SendEmail(emailDTO);

                return new ResponseDTO { Status = 200, Message = "Nurse assigned successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 400, Message = ex.Message };
            }
        }



    }
}

