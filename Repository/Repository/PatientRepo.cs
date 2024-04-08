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

    }
}
