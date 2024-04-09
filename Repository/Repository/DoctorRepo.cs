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
