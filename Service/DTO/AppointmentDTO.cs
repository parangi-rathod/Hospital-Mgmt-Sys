using Repository.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class AppointmentDTO
    {       
        public int PatientId { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public string PatientProblem { get; set; }
        public string? Description { get; set; }
        public string AppointmentStatus { get; set; }
        public string ConsultDoctor { get; set; }
        public int? NurseId { get; } = null;
    }
}
