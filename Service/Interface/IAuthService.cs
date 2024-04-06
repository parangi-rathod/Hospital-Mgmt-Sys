using Service.DTO;

namespace Service.Interface
{
    public interface IAuthService
    {
        Task<ResponseDTO> RegisterUser(RegisterUserDTO registerDTO);
        Task<ResponseDTO> RegisterDoctor(RegisterDoctorDTO registerDTO);
        Task<ResponseDTO> Login(LoginDTO loginDTO);
        //Task<ResponseDTO> ResetPassword(ResetPasswordDTO resetPasswordDTO);
    }
}
