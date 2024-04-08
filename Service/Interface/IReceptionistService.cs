using Service.DTO;

namespace Service.Interface
{
    public interface IReceptionistService
    {
        Task<ResponseDTO> RegisterPatient(RegisterPatientDTO registerPatientDTO);
        Task<ResponseDTO> ScheduleAppointment(AppointmentDTO appointmentDTO);
    }
}
