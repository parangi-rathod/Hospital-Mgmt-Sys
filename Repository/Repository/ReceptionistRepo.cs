using Repository.Interface;
using Repository.Model;

namespace Repository.Repository
{
    public class ReceptionistRepo : IReceptionistRepo
    {
        #region props
        public readonly AppDbContext _context;

        #endregion

        #region ctor
        public ReceptionistRepo(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<bool> ScheduleAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
