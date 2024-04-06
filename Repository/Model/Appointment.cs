using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public enum Status
    {
        Scheduled,
        Cancelled,
        Rescheduled
    }
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int PatientId { get; set; }
        public virtual Users Patient { get; set; }
        [Required]
        public DateTime ScheduleStartTime { get; set; }
        [Required]
        public DateTime ScheduleEndTime { get; set; }
        [Required]
        public string PatientProblem { get; set; }
        public string? Description { get; set; }
        [Required]
        public Status AppointmentStatus { get; set;}
        [Required]
        public string ConsultDoctor { get; set; }

        [ForeignKey("User")]
        public int? NurseId { get; set; }
        public virtual Users Nurse { get; set; }

    }
}
