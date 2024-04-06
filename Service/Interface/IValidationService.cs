using Service.DTO;

namespace Service.Interface
{
    public interface IValidationService
    {
        Task<ValidationDTO> ValidateRegister(RegisterUserDTO registerDTO);
        Task<ValidationDTO> ValidateRegister(RegisterDoctorDTO registerDTO);
    }
}
