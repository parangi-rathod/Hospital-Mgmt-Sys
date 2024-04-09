using Service.DTO;

namespace Service.Interface
{
    public interface IDoctorService
    {
        Task<List<dynamic>> GetDoctorAppointments(int doctorId);
    }
}
