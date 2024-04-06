namespace Service.Interface
{
    public interface IJWTTokenService
    {
        string GenerateJwtToken(string userRole);
    }
}
