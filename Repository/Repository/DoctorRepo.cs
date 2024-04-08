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
        public async Task<Users> DoctorBySpecialization(string spec)
        {
            var isExists = await _context.SpecialistDoctors.FirstOrDefaultAsync(u => u.Specialization.Equals(spec));

            if (isExists != null)
            {
                var doctor = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(isExists.UserId));
                return doctor;
            }
            else
            {
                return null; 
            }
        }

        public async Task<bool> isSpecialistDoctorExists(string? specialization)
        {
            if (specialization == null)
            {
                return false;
            }
            return await _context.SpecialistDoctors.AnyAsync(sd => sd.Specialization == specialization);
        }

        public async Task<bool> checkAvailability(string spec, DateTime startTime)
        {
            bool isAvailability = await _context.Appointments.AnyAsync(u => u.ConsultDoctor.Equals(spec) && u.ScheduleEndTime < startTime);
            return isAvailability;
        }
    }
}
