using System.ComponentModel.DataAnnotations;

public enum RoleType
{
    Doctor = 1,
    Nurse,
    Receptionist,
    Patient
}

namespace Repository.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ContactNum { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public RoleType Role { get; set; }
    }
}
