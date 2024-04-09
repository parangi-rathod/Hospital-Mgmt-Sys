using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class RescheduleAppoDTO
    {
        public int AppointmentId { get; set; }
        public DateTime NewStartTime { get; set; } = DateTime.Now;
        public DateTime NewEndTime { get; set; }
    }
}
