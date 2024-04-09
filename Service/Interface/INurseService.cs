using Service.DTO;

namespace Service.Interface
{
    public interface INurseService
    {
        Task<NurseDTO> checkDuties();
    }
}
