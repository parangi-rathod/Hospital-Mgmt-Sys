namespace Service.DTO
{
    public class NurseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNum { get; set; }
        public string Gender { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public string PatientProblem { get; set; }
        public string? Description { get; set; }
        public int DoctorId { get; set; }
    }
}
