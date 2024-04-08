using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;

namespace Repository.Repository
{
    public class UserRepo : IUserRepo
    {
        #region props
        public readonly AppDbContext _context;

        #endregion

        #region ctor
        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        #endregion
        public async Task<bool> UserExistsByContactNum(string contactNum)
        {
            bool isExists = await _context.Users.AnyAsync(u => u.ContactNum == contactNum);
            return isExists;
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            bool isExists = await _context.Users.AnyAsync(u => u.Email == email);
            return isExists;
        }
        public async Task<Users> UserById(int id)
        {
            var isExists = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            return isExists;
        }
        public async Task<int> DoctorCount()
        {
            int docCount = await _context.Users.CountAsync(u => u.Role.Equals(RoleType.Doctor));
            return docCount;
        }

        public async Task<int> NurseCount()
        {
            int nurseCount = await _context.Users.CountAsync(u => u.Role.Equals(RoleType.Nurse));
            return nurseCount;
        }

        public async Task<int> ReceptionistCount()
        {
            int recCount = await _context.Users.CountAsync(u => u.Role.Equals(RoleType.Receptionist));
            return recCount;
        }
        public async Task<bool> isSpecialistDoctorExists(string? specialization)
        {
            if (specialization == null)
            {
                return false; 
            }
            return await _context.SpecialistDoctors.AnyAsync(sd => sd.Specialization == specialization);
        }
        
    }
}