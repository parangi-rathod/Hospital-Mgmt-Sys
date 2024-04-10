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

        #region get patient by id
        public async Task<Users> GetPatinetById(int id)
        {
            var isExists = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id) && u.Role.Equals(RoleType.Patient));
            return isExists;
        }
        #endregion

        #region is patient exists

        public async Task<Users> IsPatientExists(string name, DateTime dob, string email)
        {
            var existingPatient = await _context.Users.FirstOrDefaultAsync(u =>
                u.FirstName.Equals(name) &&
                u.DateOfBirth.Date == dob.Date &&  
                u.Email.Equals(email));

            if(existingPatient != null)
            {
                return existingPatient;
            }
            return null;
        }
        #endregion

        #region get current appointment
        public async Task<Appointment> GetCurrentAppointment(int patientId, DateTime currTime)
        {
            return await _context.Appointments
               .Where(a => a.PatientId == patientId && a.ScheduleStartTime >= DateTime.Now)
               .OrderByDescending(a => a.ScheduleStartTime)
               .FirstOrDefaultAsync();
        }
        #endregion

        #region appointment history
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
        #endregion
    }
}
