﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int PatientId { get; set; }
        public virtual Users Patient { get; set; }
        [Required]
        [Column(TypeName ="datetime")]
        public DateTime ScheduleStartTime { get; set; }
        [Required]
        [Column(TypeName ="datetime")]
        public DateTime ScheduleEndTime { get; set; }
        [Required]
        public string PatientProblem { get; set; }
        public string? Description { get; set; }
        [Required]
        public string AppointmentStatus { get; set;}
        [Required]
        public string ConsultDoctor { get; set; }

        [ForeignKey("User")]
        public int? NurseId { get; set; }
        public virtual Users Nurse { get; set; }

    }
}