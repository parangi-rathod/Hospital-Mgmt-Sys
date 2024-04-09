using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;

namespace Repository.Repository
{
    public class PatientRepo : IPatientRepo
    {
        #region props
        public readonly AppDbContext _context;

        #endregion

        #region ctor
        public PatientRepo(AppDbContext context)
        {
            _context = context;
        }

       

        #endregion
        public async Task<Users> GetPatinetById(int id)
        {
            var isExists = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id) && u.Role.Equals(RoleType.Patient));
            return isExists;
        }

        public async Task<Users> IsPatientExists(string name, DateTime dob, string email)
        {
            var isExists = await _context.Users.FirstOrDefaultAsync(u => u.FirstName.Equals(name) && u.DateOfBirth.Equals(dob) && u.Email.Equals(email));
            if(isExists == null)
            {
                return null;
            }
            return isExists;
        }
        public async Task<Appointment> GetCurrentAppointment(int patientId, DateTime currTime)
        {
            // Use currTime instead of currentTime
            //Appointment currentAppointment = await _context.Appointments
            //    .FirstOrDefaultAsync(u => u.PatientId.Equals(patientId) && u.ScheduleEndTime > currTime);

            //return currentAppointment;
            return await _context.Appointments
               .Where(a => a.PatientId == patientId && a.ScheduleStartTime >= DateTime.Now)
               .OrderByDescending(a => a.ScheduleStartTime)
               .FirstOrDefaultAsync();
        }

        public async Task<List<dynamic>> AppointmentHistory(int patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.PatientId.Equals(patientId))
                .OrderByDescending(a => a.ScheduleStartTime)
                .Select(a => new 
                {
                    PatientId = "Sterling_" + a.Patient.Id.ToString(),
                    ScheduleStartTime = a.ScheduleStartTime,
                    ScheduleEndTime = a.ScheduleEndTime,
                    PatientProblem = a.PatientProblem,
                    Description = a.Description,
                })
                .ToListAsync();

            return appointments.Cast<dynamic>().ToList();
        }

    }
}
