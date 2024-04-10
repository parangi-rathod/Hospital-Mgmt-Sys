using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{

    public class SpecialistDoctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users User { get; set; }
        public string Specialization { get; set; }
    }
}
