using Service.DTO;

namespace Service.Interface
{
    public interface INurseService
    {
        Task<List<dynamic>> checkDuties(int nurseId);
    }
}
