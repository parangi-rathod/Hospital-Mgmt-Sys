using Repository.Model;
using System.Globalization;

namespace Repository.Interface
{
    public interface IPatientRepo
    {
        Task<Users> GetPatinetById(int id);
        Task<Users> IsPatientExists(string name, DateTime dob, string email);
    }
}
