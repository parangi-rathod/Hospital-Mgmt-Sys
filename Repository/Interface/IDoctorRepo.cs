using Repository.Model;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDoctorRepo
    {
        Task<string> doctorBySpecialization(int doctorId);
        //Task<bool> isSpecialistDoctorExists(string? specialization);
        Task<bool> checkAvailability(string spec, DateTime startTime);
        Task<List<dynamic>> checkAppointments(int doctorId);
    }
}
