using Service.DTO;
using Service.Interface;
using System.Text.RegularExpressions;

namespace Service.Service
{
    public class ValidationService : IValidationService
    {
        #region user validation
        public async Task<ValidationDTO> ValidateUser(RegisterUserDTO registerDTO)
        {
            var validationRules = new Dictionary<Func<RegisterUserDTO, bool>, string>
            {
                { dto => !string.IsNullOrWhiteSpace(dto.FirstName), "Firstname is required." },
                { dto => !string.IsNullOrWhiteSpace(dto.LastName), "Lastname is required." },
                { dto => !string.IsNullOrWhiteSpace(dto.Email) && IsValidEmail(dto.Email), "Invalid email format." },
                { dto => string.IsNullOrWhiteSpace(dto.ContactNum) || IsValidPhoneNumber(dto.ContactNum), "Invalid contact format."},
                { dto => !string.IsNullOrWhiteSpace(dto.Address), "Invalid address"},
                { dto => !string.IsNullOrWhiteSpace(dto.Gender) && ValidGender(dto.Gender), "Gender must be male, female or other"},
                { dto => string.IsNullOrWhiteSpace(dto.Pincode) || Regex.IsMatch(dto.Pincode, @"^\d{6}$"), "Pincode must be lenght of 6 numbers"},
                { dto=> dto.DateOfBirth<DateTime.Today, "Date of Birth should be less than today's date"},
                { dto => dto.DateOfBirth.Month <= 12, "Invalid month" },
                { dto => dto.DateOfBirth.Day <= DateTime.DaysInMonth(dto.DateOfBirth.Year, dto.DateOfBirth.Month), "Invalid day" },
                { dto => ValidateRole(dto.Role),"Role must be Nurse or Receptionist" }
            };

            return await ValidateDTO(registerDTO, validationRules);
        }
        #endregion

        #region patient validation
        public async Task<ValidationDTO> ValidatePatient(RegisterPatientDTO registerDTO)
        {
            var validationRules = new Dictionary<Func<RegisterPatientDTO, bool>, string>
            {
                { dto => !string.IsNullOrWhiteSpace(dto.FirstName), "Firstname is required." },
                { dto => !string.IsNullOrWhiteSpace(dto.LastName), "Lastname is required." },
                { dto => !string.IsNullOrWhiteSpace(dto.Email) && IsValidEmail(dto.Email), "Invalid email format." },
                { dto => string.IsNullOrWhiteSpace(dto.ContactNum) || IsValidPhoneNumber(dto.ContactNum), "Invalid contact format."},
                { dto => !string.IsNullOrWhiteSpace(dto.Address), "Invalid address"},
                { dto => !string.IsNullOrWhiteSpace(dto.Gender) && ValidGender(dto.Gender), "Gender must be male, female or other"},
                { dto => string.IsNullOrWhiteSpace(dto.Pincode) || Regex.IsMatch(dto.Pincode, @"^\d{6}$"), "Pincode must be length of 6 numbers"},
            };

            return await ValidateDTO(registerDTO, validationRules);
        }
        #endregion

        #region doctor validation

        public async Task<ValidationDTO> ValidateDoctor(RegisterDoctorDTO registerDTO)
        {
            var validationRules = new Dictionary<Func<RegisterDoctorDTO, bool>, string>
            {
                { dto => !string.IsNullOrWhiteSpace(dto.FirstName), "Firstname is required." },
                { dto => !string.IsNullOrWhiteSpace(dto.LastName), "Lastname is required." },
                { dto => !string.IsNullOrWhiteSpace(dto.Email) && IsValidEmail(dto.Email), "Invalid email format." },
                { dto => string.IsNullOrWhiteSpace(dto.ContactNum) || IsValidPhoneNumber(dto.ContactNum), "Invalid contact format."},
                { dto => !string.IsNullOrWhiteSpace(dto.Address), "Invalid address"},
                { dto => !string.IsNullOrWhiteSpace(dto.Gender) && ValidGender(dto.Gender), "Gender must be male, female or other"},
                { dto => string.IsNullOrWhiteSpace(dto.Pincode) || Regex.IsMatch(dto.Pincode, @"^\d{6}$"), "Pincode must be lenght of 6 numbers"},
                { dto => !string.IsNullOrWhiteSpace(dto.Specialization) && ValidateSpecialization(dto.Specialization), "Doctor specialization must be EyeSpecialist, Physiotherapist, or BrainSurgen" }

            };
            return await ValidateDTO(registerDTO, validationRules);
        }
        #endregion

        #region appointment validation
        public async Task<ValidationDTO> ValidateAppointment(AppointmentDTO appointmentDTO)
        {
            var validationRules = new Dictionary<Func<AppointmentDTO, bool>, string>
            {
                { dto => dto.PatientId > 0, "Patient ID is required." },
                { dto => dto.ScheduleStartTime != default(DateTime), "Schedule start time is required." },
                { dto => dto.ScheduleEndTime != default(DateTime), "Schedule end time is required." },
                { dto => dto.ScheduleEndTime > dto.ScheduleStartTime, "Schedule end time must be greater than start time." },
                { dto => !string.IsNullOrWhiteSpace(dto.PatientProblem), "Patient problem is required." },
                { dto => IsValidAppointmentStatus(dto.AppointmentStatus), "Invalid appointment status." },
            };

            return await ValidateDTO(appointmentDTO, validationRules);
        }
        #endregion

        #region generic method
        // Generic validation method
        private async Task<ValidationDTO> ValidateDTO<T>(T dto, Dictionary<Func<T, bool>, string> validationRules)
        {
            ValidationDTO response = new ValidationDTO();
            response.Status = 200;
            response.Message = "Validation successful.";

            List<string> errors = new List<string>();

            foreach (var rule in validationRules)
            {
                if (!rule.Key(dto))
                {
                    errors.Add($"Validation failed: {rule.Value}");
                }
            }

            if (errors.Any())
            {
                response.Status = 400;
                response.Message = "Validation failed.";
                response.Errors = errors;
            }

            return response;
        }

        private bool IsValidEmail(string email)
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
        #endregion

        #region private methods
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.Match(phoneNumber, @"^\d{10}$").Success;
        }

        private bool ValidGender(string gender)
        {
            string lowerCaseGender = gender.ToLower();
            return lowerCaseGender == "female" || lowerCaseGender == "male" || lowerCaseGender == "other";
        }
        private bool IsValidAppointmentStatus(string status)
        {
            var validStatuses = new List<string> { "Scheduled", "Cancelled", "Rescheduled" };
            return validStatuses.Contains(status);
        }

        private bool ValidateRole(string role)
        {
            string lowercaseRole = role.ToLower();
            return Enum.TryParse(lowercaseRole, true, out RoleType _);
        }

        private bool ValidateSpecialization(string specialization)
        {
            string lowerCaseSpec = specialization.ToLower();
            if (lowerCaseSpec == "eyespecialist" || lowerCaseSpec == "physiotherapist" || lowerCaseSpec == "brainsurgen")
            {
                return true;
            }
            return false;
        }
        #endregion
    }

}
