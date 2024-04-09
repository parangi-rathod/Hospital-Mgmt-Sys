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
        public async Task<bool> checkAvailability(string spec, DateTime startTime)
        {
            bool isAvailability = await _context.Appointments.AnyAsync(u => u.ConsultDoctor.Equals(spec) && u.ScheduleEndTime < startTime);
            return isAvailability;
        }

        public async Task<List<dynamic>> checkAppointments(int doctorId)
        {
            var currentTime = DateTime.Now;

            var appointments = await _context.Appointments
                .Where(a => a.ConsultDoctorId == doctorId && a.ScheduleEndTime > currentTime)
                .Join(
                    _context.Users, // Join with the Users table
                    appointment => appointment.PatientId, // Match appointment patient ID
                    user => user.Id, // With user ID
                    (appointment, user) => new // Select anonymous object with desired properties
                    {
                        user.FirstName,
                        user.LastName,
                        user.ContactNum,
                        user.Gender,
                        appointment.ScheduleStartTime,
                        appointment.ScheduleEndTime,
                        appointment.AppointmentStatus,
                        appointment.PatientProblem,
                        appointment.Description
                    }
                )
                .ToListAsync();

            return appointments.Cast<dynamic>().ToList();
        }


        //    var appointments = await _context.Appointments
        //.Where(a => a.ConsultDoctorId == doctorId && a.ScheduleEndTime > time)
        //.Include(a => a.Patient)
        //.Select(a => new AppointmentDTO
        //{
        //    FirstName = a.Patient.FirstName,
        //    LastName = a.Patient.LastName,
        //    ContactNum = a.Patient.ContactNum,
        //    Gender = a.Patient.Gender,
        //    ScheduleStartTime = a.ScheduleStartTime,
        //    ScheduleEndTime = a.ScheduleEndTime,
        //    Status = a.AppointmentStatus,
        //    PatientProblem = a.PatientProblem,
        //    Description = a.Description
        //})
        //.ToListAsync();

        public Task<Users> doctorBySpecialization(string spec)
        {
            throw new NotImplementedException();
        }
    }
}
