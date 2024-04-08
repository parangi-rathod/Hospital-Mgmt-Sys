using Newtonsoft.Json;

namespace Service.DTO
{
    public class RegisterPatientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; }
        public string ContactNum { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Role { get; } = "Patient";

    }

}
