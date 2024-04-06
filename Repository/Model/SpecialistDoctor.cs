using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
   
    public class SpecialistDoctor
    {
        [Required]
        [ForeignKey("Users")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual Users User { get; set; } 
        public string Specialization { get; set; }
    }
}
