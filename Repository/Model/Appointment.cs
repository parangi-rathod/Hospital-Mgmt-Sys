using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int PatientId { get; set; }
        public virtual Users Patient { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ScheduleStartTime { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ScheduleEndTime { get; set; }

        [Required]
        public string PatientProblem { get; set; }

        public string Description { get; set; }
        [MaxLength(50)]
        public string Description1 { get; set; }

        [Required]
        public string AppointmentStatus { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int ConsultDoctorId { get; set; } // Change ConsultDoctor to ConsultDoctorId
        public virtual Users ConsultDoctor { get; set; } // Add navigation property for the doctor

        public int? NurseId { get; set; }
        public virtual Users Nurse { get; set; }
    }
}
