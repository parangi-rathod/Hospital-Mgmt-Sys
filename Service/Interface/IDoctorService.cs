using Service.DTO;

namespace Service.Interface
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetDoctorAppointments();
    }
}
