using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class DoctorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNum { get; set; }
        public string Gender { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public string Status { get; set; }
        public string PatientProblem { get; set; }
        public string? Description { get; set; }
    }
}
