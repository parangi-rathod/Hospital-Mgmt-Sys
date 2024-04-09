using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Repository.Model;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PatientService : IPatientService
    {
        #region properties
        
        private readonly IPatientRepo _patientRepo;
       

        #endregion

        #region ctor
        public PatientService(IPatientRepo patientRepo) {
           
            _patientRepo = patientRepo;            
        }

        

        #endregion

        public async Task<ResponseDTO> getCurrentAppointment(int patientId)
        {
            try
            {
                DateTime currTime = DateTime.Now;
                // Await the asynchronous method to get the actual Appointment object
                Appointment currentAppo = await _patientRepo.GetCurrentAppointment(patientId, currTime);

                // Check if currentAppo is null
                if (currentAppo == null)
                {
                    return new ResponseDTO { Status = 400, Message = "No current appointment" };
                }

                return new ResponseDTO { Status = 200, Data = currentAppo, Message = "Current appointment" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 400, Message = ex.Message };
            }
        }

        public async Task<ResponseDTO> appointmentHistory(int patientId)
        {
            try
            {
                List<dynamic> appoHist = await _patientRepo.AppointmentHistory(patientId);
                if (appoHist == null)
                {
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "No Previous appointments"
                    };
                }
                return new ResponseDTO
                {
                    Status = 200,
                    Data = appoHist,
                    Message = "Previous appointments"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { Status = 500, Message = ex.Message };
            }
        }
    }
}
