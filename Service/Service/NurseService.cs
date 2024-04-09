using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Service.DTO;
using Service.Interface;

namespace Service.Service
{
    public class NurseService : INurseService
    {
        #region properties
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IAuthRepo _authRepo;
        private readonly IReceptionistRepo _receptionistRepo;
        private readonly IUserRepo _userRepo;
        private readonly IDoctorRepo _docRepo;
        private readonly INurseRepo _nurseRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IPasswordHash _passwordHash;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpCon;
        private readonly IJWTTokenService _jwtToken;

        #endregion

        #region ctor
        public NurseService(IAuthRepo authRepo, INurseRepo nurseRepo, IJWTTokenService jwtToken, IMapper mapper, IEmailService emailService, IPatientRepo patientRepo, IReceptionistRepo receptionistRepo, IUserRepo userRepo, IDoctorRepo docRepo, IPasswordHash passwordHash, IValidationService validationService)
        {
            _mapper = mapper;
            _emailService = emailService;
            _authRepo = authRepo;
            _userRepo = userRepo;
            _docRepo = docRepo;
            _nurseRepo = nurseRepo;
            _patientRepo = patientRepo;
            _receptionistRepo = receptionistRepo;
            _passwordHash = passwordHash;
            _validationService = validationService;
        }



        #endregion
        public async Task<List<dynamic>> checkDuties(int nurseId)
        {
            try
            {
                var nurseAppointments = await _nurseRepo.nurseDuties(nurseId);
                //no appointments also
                return nurseAppointments;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }
    }
}
