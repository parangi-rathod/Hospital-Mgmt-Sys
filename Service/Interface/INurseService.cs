using Service.DTO;

namespace Service.Interface
{
    public interface INurseService
    {
        Task<ResponseDTO> checkDuties();
    }
}
