using Service.DTO;

namespace Service.Interface
{
    public interface IValidationService
    {
        Task<ValidationDTO> ValidateUser(RegisterUserDTO registerDTO);
        Task<ValidationDTO> ValidateDoctor(RegisterDoctorDTO registerDTO);
        Task<ValidationDTO> ValidatePatient(RegisterPatientDTO registerDTO);
        Task<ValidationDTO> ValidateAppointment(AppointmentDTO appointmentDTO);

    }
}
