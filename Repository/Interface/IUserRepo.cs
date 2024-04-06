using Repository.Model;

namespace Repository.Interface
{
    public interface IUserRepo
    {
        Task<bool> UserExistsByEmail(string email);
        Task<bool> UserExistsByContactNum(string contactNum);
        Task<int> DoctorCount();
        Task<int> NurseCount();
        Task<int> ReceptionistCount();
        Task<bool> isSpecialistDoctorExists(string? specialization);
    }
}
