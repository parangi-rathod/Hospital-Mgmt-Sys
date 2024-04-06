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
            CreateMap<RegisterDoctorDTO, Users>()
            .ConstructUsing((src, ctx) => new Users()) // Create new instance of Users
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => RoleType.Doctor));

        }
        private RoleType MapRole(string role)
        {
            // Implement logic to map string role to RoleType enum
            // You can define your own mapping logic based on your requirements
            // For example:
            switch (role.ToLower())
            {
                case "doctor":
                    return RoleType.Doctor;
                case "nurse":
                    return RoleType.Nurse;
                // Add more cases as needed
                default:
                    throw new ArgumentException($"Invalid role: {role}");
            }
        }
    }
}
