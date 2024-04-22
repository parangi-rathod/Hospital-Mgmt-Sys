using Repository.Model;

namespace Repository.Interface
{
    public interface IAuthRepo
    {
        Task<Users> Register(Users user);
        Task<Users> Remove(Users user);
        Task<bool> SpecialistDoctorReg(SpecialistDoctor specialistDoctor);
        Task<Users> Login(string username, string password);
    }
}
