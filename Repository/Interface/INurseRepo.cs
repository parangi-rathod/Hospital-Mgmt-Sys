namespace Repository.Interface
{
    public interface INurseRepo
    {
        Task<bool> isNurseExists(int nurseId);
        Task<List<dynamic>> nurseDuties(int nurseId);
    }
}
