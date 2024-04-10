using Service.DTO;

namespace Service.Interface
{
    public interface IPatientService
    {
        Task<ResponseDTO> getCurrentAppointment();
        Task<ResponseDTO> appointmentHistory();
    }
}
