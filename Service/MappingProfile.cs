using AutoMapper;
using Repository.Model;
using Service.DTO;

namespace Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDTO, Users>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => MapRole(src.Role))).ReverseMap();
            CreateMap<RegisterPatientDTO, Users>().ReverseMap();
            CreateMap<RegisterDoctorDTO, Users>()
            .ConstructUsing((src, ctx) => new Users()) 
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => RoleType.Doctor));
            CreateMap<AppointmentDTO, Appointment>().ReverseMap();

        }
        private RoleType MapRole(string role)
        {
            switch (role.ToLower())
            {
                case "nurse":
                    return RoleType.Nurse;
                case "receptionist":
                    return RoleType.Receptionist;
                default:
                    throw new ArgumentException($"Invalid role: {role}");
            }
        }
    }
}
