namespace Repository.Interface
{
    public interface INurseRepo
    {
        Task<string> isNurseExists(int nurseId);
        Task<List<dynamic>> nurseDuties(int nurseId);
    }
}
