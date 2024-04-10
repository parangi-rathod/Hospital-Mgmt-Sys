namespace Service.Interface
{
    public interface IPasswordHash
    {
        string GeneratePasswordHash(string password);
    }
}
