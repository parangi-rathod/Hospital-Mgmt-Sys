using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Repository.Interface;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DoctorService : IDoctorService
    {
        #region properties
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IAuthRepo _authRepo;
        private readonly IReceptionistRepo _receptionistRepo;
        private readonly IUserRepo _userRepo;
        private readonly IDoctorRepo _docRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IPasswordHash _passwordHash;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpCon;
        private readonly IJWTTokenService _jwtToken;

        #endregion

        #region ctor
        public DoctorService(IAuthRepo authRepo, IJWTTokenService jwtToken, IMapper mapper, IEmailService emailService, IPatientRepo patientRepo, IReceptionistRepo receptionistRepo, IUserRepo userRepo, IDoctorRepo docRepo, IPasswordHash passwordHash, IValidationService validationService)
        {
            _mapper = mapper;
            _emailService = emailService;
            _authRepo = authRepo;
            _userRepo = userRepo;
            _docRepo = docRepo;
            _patientRepo = patientRepo;
            _receptionistRepo = receptionistRepo;
            _passwordHash = passwordHash;
            _validationService = validationService;
        }

        #endregion
        public async Task<List<DoctorDTO>> GetDoctorAppointments()
        {
            try
            {
                //var userClaim = _httpCon.Users.
                //if (userIdClaim == null || !int.TryParse(userIdClaim, out int doctorId))
                //{
                //    throw new ArgumentException("Invalid or missing doctor ID in claims.");
                //}

                // Call the repository method to retrieve doctor appointments
                int docId = 3;
                var doctorAppointments = await _docRepo.checkAppointments(docId);

                // Convert the appointments to DoctorDTO objects
                var doctorDTOs = doctorAppointments.Select(appointment => new DoctorDTO
                {
                    FirstName = appointment.FirstName,
                    LastName = appointment.LastName,
                    ContactNum = appointment.ContactNum,
                    Gender = appointment.Gender,
       
                }).ToList();

                return doctorDTOs;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }





    }
}
