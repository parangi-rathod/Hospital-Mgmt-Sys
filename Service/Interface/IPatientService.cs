using Service.DTO;

namespace Service.Interface
{
    public interface IPatientService
    {
        Task<ResponseDTO> getCurrentAppointment(int patientId);
        Task<ResponseDTO> appointmentHistory(int patientId);
    }
}
