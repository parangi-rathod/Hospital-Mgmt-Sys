using Repository.Model;
using Repository.Repository;
using Service.DTO;

namespace Service.Interface
{
    public interface IDoctorService
    {
        Task<ResponseDTO> getDoctorAppointments(int doctorId);
        Task<ResponseDTO> rescheduleAppointment(RescheduleAppoDTO rescheduleAppoDTO, int doctorId);
        Task<ResponseDTO> assignNurse(AssignNurseDTO assignNurseDTO, int doctorId);
        Task<ResponseDTO> cancelAppointment(int appointmentId, int docId);
    }
}
