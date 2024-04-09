using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;

namespace Repository.Repository
{
    public class AuthRepo : IAuthRepo
    {
        #region props
        public readonly AppDbContext _context;
        public readonly IUserRepo _userRepo;

        #endregion

        #region ctor
        public AuthRepo(AppDbContext context, IUserRepo userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }
        #endregion

        public async Task<Users> Register(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> SpecialistDoctorReg(SpecialistDoctor specialistDoctor)
        {
            _context.SpecialistDoctors.Add(specialistDoctor);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Users> Login(string username, string password)
        {
            bool isUserExistsByCon = await _userRepo.UserExistsByContactNum(username);
            bool isUserExistsByEmail = await _userRepo.UserExistsByEmail(username);

            if (isUserExistsByCon || isUserExistsByEmail)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => (u.ContactNum.Equals(username) || u.Email.Equals(username)) && u.Password.Equals(password));
                return user;
            }
            return null;
        } 

    }
}
