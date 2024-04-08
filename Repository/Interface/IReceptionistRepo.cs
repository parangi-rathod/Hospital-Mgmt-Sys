using Repository.Model;

namespace Repository.Interface
{
    public interface IReceptionistRepo
    {
        Task<bool> ScheduleAppointment(Appointment appointment);
    }
}
