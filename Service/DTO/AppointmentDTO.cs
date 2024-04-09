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
        public int ConsultDoctorId { get; set; }
    }
}
