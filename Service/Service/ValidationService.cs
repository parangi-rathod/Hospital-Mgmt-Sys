using Repository.Model;
using Service.DTO;
using Service.Interface;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Service.Service
{
    public class ValidationService : IValidationService
    {
        public async Task<ValidationDTO> ValidateRegister(RegisterUserDTO registerDTO)
        {
            ValidationDTO response = new ValidationDTO();
            response.Status = 200;
            response.Message = "Validation successful.";

            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(registerDTO.FirstName))
            {
                errors.Add("Firstname is required.");
            }

            if (string.IsNullOrWhiteSpace(registerDTO.LastName))
            {
                errors.Add("Lastname is required.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Email))
            {
                errors.Add("Email is required.");
            }
            else if (!IsValidEmail(registerDTO.Email))
            {
                errors.Add("Invalid email format.");
            }
            if (!string.IsNullOrWhiteSpace(registerDTO.ContactNum) && !IsValidPhoneNumber(registerDTO.ContactNum))
            {
                errors.Add("Invalid contact number format.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Address))
            {
                errors.Add("Address is required.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Gender))
            {
                errors.Add("Gender is required.");
            }
            if (!ValidGender(registerDTO.Gender))
            {
                errors.Add("Enter valid gender.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Pincode))
            {
                errors.Add("Pincode is required.");
            }
            if (!Regex.IsMatch(registerDTO.Pincode, @"^\d{6}$"))
            {
                errors.Add("Pincode must be a 6-digit number.");
            }
            if (!ValidateRole(registerDTO.Role))
            {
                errors.Add("Role must be either of Doctor, nurse, receptionist or Patient");
            }

            bool ValidGender(string gender)
            {
                // Check if the gender is one of the accepted values
                if (gender != "Female" && gender != "Male" && gender != "Other")
                {
                    return false; // If the gender is not one of the accepted values, return false
                }
                return true; // Otherwise, return true
            }


            bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
            bool IsValidPhoneNumber(string phoneNumber)
            {
                return Regex.Match(phoneNumber, @"^\d{10}$").Success;
            }

            bool ValidateRole(string role)
            {
               return Enum.IsDefined(typeof(RoleType), role);
            }


            if (errors.Any())
            {
                response.Status = 400;
                response.Message = "Validation failed.";
                response.Errors = errors;
            }
            return await Task.FromResult(response);
        }

        public async Task<ValidationDTO> ValidateRegister(RegisterDoctorDTO registerDTO)
        {
            ValidationDTO response = new ValidationDTO();
            response.Status = 200;
            response.Message = "Validation successful.";

            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(registerDTO.FirstName))
            {
                errors.Add("Firstname is required.");
            }

            if (string.IsNullOrWhiteSpace(registerDTO.LastName))
            {
                errors.Add("Lastname is required.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Email))
            {
                errors.Add("Email is required.");
            }
            else if (!IsValidEmail(registerDTO.Email))
            {
                errors.Add("Invalid email format.");
            }
            if (!string.IsNullOrWhiteSpace(registerDTO.ContactNum) && !IsValidPhoneNumber(registerDTO.ContactNum))
            {
                errors.Add("Invalid contact number format.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Address))
            {
                errors.Add("Address is required.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Gender))
            {
                errors.Add("Gender is required.");
            }
            if (!ValidGender(registerDTO.Gender))
            {
                errors.Add("Enter valid gender.");
            }
            if (string.IsNullOrWhiteSpace(registerDTO.Pincode))
            {
                errors.Add("Pincode is required.");
            }
            if (!Regex.IsMatch(registerDTO.Pincode, @"^\d{6}$"))
            {
                errors.Add("Pincode must be a 6-digit number.");
            }
            if (registerDTO.Specialization != "EyeSpecialist" && registerDTO.Specialization != "Physiotherapist" && registerDTO.Specialization != "BrainSurgen")
            {
                errors.Add("Doctor specialization must be EyeSpecialist, Physiotherapist or BrainSurgen");
            }

            bool ValidGender(string gender)
            {
                string lowerCaseGender = gender.ToLower();
                if (lowerCaseGender != "female" && lowerCaseGender != "male" && lowerCaseGender != "other")
                {
                    return false;
                }
                return true;
            }



            bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
            bool IsValidPhoneNumber(string phoneNumber)
            {
                return Regex.Match(phoneNumber, @"^\d{10}$").Success;
            }

            if (errors.Any())
            {
                response.Status = 400;
                response.Message = "Validation failed.";
                response.Errors = errors;
            }
            return await Task.FromResult(response);
        }
    }
}
