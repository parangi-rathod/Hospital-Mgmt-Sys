using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Repository
{
    public class NurseRepo : INurseRepo
    {
        #region props
        public readonly AppDbContext _context;

        #endregion

        #region ctor
        public NurseRepo(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<string> isNurseExists(int nurseId)
        {
            var isExists = await _context.Users.FirstOrDefaultAsync(u=>u.Id.Equals(nurseId) && u.Role.Equals(RoleType.Nurse));
            return isExists.Email;
        }

        public async Task<List<dynamic>> nurseDuties(int nurseId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.NurseId.Equals(nurseId))
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
                })
                .ToListAsync();

            return appointments.Cast<dynamic>().ToList();
        }


    }
}