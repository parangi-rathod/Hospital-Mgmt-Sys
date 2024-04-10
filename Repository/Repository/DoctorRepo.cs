using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;

namespace Repository.Repository
{
    public class DoctorRepo : IDoctorRepo
    {
        #region props
        public readonly AppDbContext _context;

        #endregion

        #region ctor
        public DoctorRepo(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        #region doctor by specialization
        public async Task<string> doctorBySpecialization(int doctorId)
        {
            var specialistDoctor = await _context.SpecialistDoctors
                .Include(sd => sd.User) 
                .FirstOrDefaultAsync(sd => sd.UserId == doctorId);

            if (specialistDoctor != null)
            {
                return specialistDoctor.Specialization;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region check avaiability
        public async Task<bool> checkAvailability(int doctorId, DateTime startTime, DateTime endTime)
        {
            bool isAvailable = await _context.Appointments.AnyAsync(u =>
                u.ConsultDoctorId == doctorId &&
                u.ScheduleStartTime < endTime && 
                u.ScheduleEndTime > startTime); 
            return !isAvailable;
        }
        #endregion

        #region check appointments
        public async Task<List<dynamic>> checkAppointments(int doctorId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.ConsultDoctorId == doctorId)
                .OrderByDescending(a => a.ScheduleStartTime)
                .Select(a => new
                {
                    patientId = "Sterling_" + a.Patient.Id.ToString(),
                    patientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    gender = a.Patient.Gender.ToString(),
                    email = a.Patient.Email,
                    phoneNumber = a.Patient.ContactNum,
                    scheduleStartTime = a.ScheduleStartTime,
                    scheduleEndTime = a.ScheduleEndTime,
                    patientProblem = a.PatientProblem,
                    description = a.Description,
                    status = a.AppointmentStatus.ToString()
                })
                .ToListAsync();

            return appointments.Cast<dynamic>().ToList();
        }
        #endregion

        #region is appointment exists
       

        public async Task<bool> isAppointmentExists(int appointmentId, int docId)
        {
            var isExists = await _context.Appointments.AnyAsync(u => u.Id.Equals(appointmentId) && u.ConsultDoctorId.Equals(docId));
            return isExists;
        }
        #endregion

        #region reschedule appointment
        public async Task<string> rescheduleAppointment(int appointmentId, DateTime startTime, DateTime endTime)
        {
            var updateAppo = await _context.Appointments.FirstOrDefaultAsync(u => u.Id.Equals(appointmentId));

            updateAppo.AppointmentStatus = "Rescheduled";
            updateAppo.ScheduleStartTime = startTime;
            updateAppo.ScheduleEndTime = endTime;

            var patientId = updateAppo.PatientId;
            _context.Update(updateAppo);
            await _context.SaveChangesAsync();

            var patient = await _context.Users.FirstOrDefaultAsync(u=>u.Id.Equals(patientId));
           
            return patient.Email;

        }
        #endregion

        #region cancel appointment

        public async Task<string> cancelAppointment(int appointmentId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(u => u.Id.Equals(appointmentId));
            appointment.AppointmentStatus = "Cancelled";
            var patientId = appointment.PatientId;
            _context.Update(appointment);
            await _context.SaveChangesAsync();
            var patient = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(patientId));
            string patientEmail = patient.Email;
            return patientEmail;
        }
        #endregion

        #region assign nurse
        public async Task<dynamic> assignNurse(int nurseId, int appointmentId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(u => u.Id == appointmentId);

            appointment.NurseId = nurseId;
            _context.Update(appointment);
            await _context.SaveChangesAsync();

            var patient = await _context.Users.FirstOrDefaultAsync(u => u.Id == appointment.PatientId);

            dynamic result = new
            {
                apointmentId = appointment.Id,
                scheduleStartTime = appointment.ScheduleStartTime,
                scheduleEndTime=appointment.ScheduleEndTime,
                patientProb = appointment.PatientProblem,
                description = appointment.Description,
                firstname = patient.FirstName,
                lastname = patient.LastName,
                gender = patient.Gender,
                email = patient.Email,
                contactNum=patient.ContactNum
            };

            return (dynamic)result;
        }
        #endregion
    }
}
