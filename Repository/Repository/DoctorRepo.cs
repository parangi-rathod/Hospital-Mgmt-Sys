using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;
using System.Dynamic;

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


        public async Task<string> doctorBySpecialization(int doctorId)
        {
            var specialistDoctor = await _context.SpecialistDoctors
                .Include(sd => sd.User) // Include the associated user details
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


        //public async Task<bool> isSpecialistDoctorExists(string? specialization)
        //{
        //    if (specialization == null)
        //    {
        //        return false;
        //    }
        //    //return await _context.SpecialistDoctors.AnyAsync(sd => sd.Specialization == specialization);
        //}
        public async Task<bool> checkAvailability(int doctorId, DateTime startTime)
        {
            bool isAvailability = await _context.Appointments.AnyAsync(u => u.ConsultDoctorId.Equals(doctorId) && u.ScheduleEndTime < startTime);
            return isAvailability;
        }

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

        public Task<Users> doctorBySpecialization(string spec)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isAppointmentExists(int appointmentId, int docId)
        {
            var isExists = await _context.Appointments.AnyAsync(u => u.Id.Equals(appointmentId) && u.ConsultDoctorId.Equals(docId));
            return isExists;
        }
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
            //var rescheduledAppointmentInfo = new
            //{
            //    ScheduleStartTime = updateAppo.ScheduleStartTime,
            //    ScheduleEndTime = updateAppo.ScheduleEndTime,
            //    PatientFirstName = patient.FirstName,
            //    PatientLastName = patient.LastName,
            //    PatientEmail = patient.Email
            //};
            return patient.Email;

            //return (dynamic)rescheduledAppointmentInfo;
        }

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
    }
}
