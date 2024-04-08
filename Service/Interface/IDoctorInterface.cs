namespace Service.Interface
{
    public interface IDoctorInterface
    {
        Task<bool> checkAvailability();
    }
}
