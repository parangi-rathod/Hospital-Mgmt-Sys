namespace Repository.Interface
{
    public interface INurseRepo
    {
        Task<bool> isNurseExists(int nurseId);
    }
}
