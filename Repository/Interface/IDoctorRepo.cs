using Repository.Model;

namespace Repository.Interface
{
    public interface IDoctorRepo
    {
        Task<Users> DoctorBySpecialization(string spec);
        Task<bool> isSpecialistDoctorExists(string? specialization);
        Task<bool> checkAvailability(string spec, DateTime startTime);
    }
}
