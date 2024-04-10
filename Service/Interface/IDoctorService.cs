using Service.DTO;

namespace Service.Interface
{
    public interface IDoctorService
    {
        Task<ResponseDTO> getDoctorAppointments();
        Task<ResponseDTO> rescheduleAppointment(RescheduleAppoDTO rescheduleAppoDTO);
        Task<ResponseDTO> assignNurse(AssignNurseDTO assignNurseDTO);
        Task<ResponseDTO> cancelAppointment(int appointmentId);
    }
}
