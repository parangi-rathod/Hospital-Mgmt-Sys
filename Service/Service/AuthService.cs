using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using Repository.Model;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class AuthService : IAuthService
    {
        #region properties
        private readonly IAuthRepo _authRepo;
        private readonly IMapper _mapper;
        //private readonly IEmailService _emailService;
        private readonly IUserRepo _userRepo;
        private readonly IJWTTokenService _jwtToken;
        private readonly IPasswordHash _passwordHash;
        private readonly IValidationService _validationService;

        #endregion

        #region ctor
        public AuthService(IAuthRepo authRepo,  IMapper mapper, IJWTTokenService jwtToken, /*IEmailService emailService*/ IUserRepo userRepo, IPasswordHash passwordHash, IValidationService validationService)
        {
            _authRepo = authRepo;
            _mapper = mapper;
            //_emailService = emailService;
            _userRepo = userRepo;
            _jwtToken = jwtToken;
            _passwordHash = passwordHash;
            _validationService = validationService;
        }

        #endregion

        #region register methods
        public async Task<ResponseDTO> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            try
            {
               
                var validationResult = await _validationService.ValidateRegister(registerUserDTO);

                if (validationResult.IsValid)
                {
                    string passHash = _passwordHash.GeneratePasswordHash(registerUserDTO.Password);
                    var user = _mapper.Map<Users>(registerUserDTO);
                    user.Password = passHash;

                    if(registerUserDTO.Role.Equals(RoleType.Nurse))
                    {
                        var nurseCount = await _userRepo.NurseCount(); 
                        if (nurseCount == 10)
                        {
                            return new ResponseDTO { Status = 400, Message = "System already contains 10 nurses." };
                        }
                    }
                    if(registerUserDTO.Role.Equals(RoleType.Receptionist))
                    {
                        var nurseCount = await _userRepo.ReceptionistCount(); 
                        if (nurseCount == 2)
                        {
                            return new ResponseDTO { Status = 400, Message = "System already contains 2 receptionist." };
                        }
                    }

                    var reg = await _authRepo.Register(user);
                    
                    return new ResponseDTO { Status = 200, Message = "User registered successfully." };
                }

                return new ResponseDTO
                {
                    Status = validationResult.Status,
                    Message = "Validation failed.",
                    Error = string.Join("; ", validationResult.Errors)
                };
            }  
            catch(Exception ex)
            {
                return new ResponseDTO { Status = 500, Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<ResponseDTO> RegisterDoctor(RegisterDoctorDTO registerDoctorDTO)
        {
            try
            {
                var doctorCount = await _userRepo.DoctorCount(); // Await the asynchronous method
                if (doctorCount == 3)
                {
                    return new ResponseDTO { Status = 400, Message = "System already contains 3 doctors." };
                }

                var validationResult = await _validationService.ValidateRegister(registerDoctorDTO);

                if (!validationResult.IsValid)
                {
                    return new ResponseDTO { Status = 400, Message = "Validation failed.", Error = string.Join("; ", validationResult.Errors) };
                }
                var specializationString = registerDoctorDTO.Specialization;
                

                if (await _userRepo.isSpecialistDoctorExists(specializationString))
                {
                    return new ResponseDTO { Status = 400, Message = "A doctor with this specialization already exists." };
                }

                string passHash = _passwordHash.GeneratePasswordHash(registerDoctorDTO.Password);
                var user = _mapper.Map<Users>(registerDoctorDTO);
                user.Password = passHash;
                
                var reg = await _authRepo.Register(user);

                
                var specialization = registerDoctorDTO.Specialization;
                if (specialization != null)
                {
                    
                    var specializationEntity = new SpecialistDoctor
                    {
                        UserId = reg.Id,
                        Specialization = specialization
                    };

                    await _authRepo.SpecialistDoctorReg(specializationEntity);
                }

                return new ResponseDTO { Status = 200, Message = "User and specialization registered successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 500, Message = $"Error: {ex.Message}" };
            }
        }
        #endregion


        public async Task<ResponseDTO> Login(LoginDTO loginDTO)
        {
            string PassHash = _passwordHash.GeneratePasswordHash(loginDTO.Password);
            var user = await _authRepo.Login(loginDTO.ContactNumOrEmail, PassHash);
            if (user != null)
            {
                string userRoleString = user.Role.ToString();
                string token = _jwtToken.GenerateJwtToken(userRoleString);

                // Now, you need to return the token as part of your ResponseDTO
                return new ResponseDTO
                {
                    Status = 200,
                    Data = token,
                    Message = "Login successful",
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Invalid email or password"
                };
            }
        }



        #region miscellaneous methods


        #endregion
    }
}
