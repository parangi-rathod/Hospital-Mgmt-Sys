using Repository.Model;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDoctorRepo
    {
      
        Task<bool> checkAvailability(int doctorId, DateTime startTime, DateTime endTime);
        Task<string> doctorBySpecialization(int doctorId);
        Task<List<dynamic>> checkAppointments(int doctorId);
        Task<string> rescheduleAppointment(int appointmentId, DateTime startTime, DateTime endTime);
        Task<bool> isAppointmentExists(int appointmentId, int docId);
        Task<dynamic> assignNurse(int nurseId, int appointmentId);
        Task<string> cancelAppointment(int appointmentId);
    }
}
