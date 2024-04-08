using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Model;

namespace Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //conversion of enum to string
            modelBuilder.Entity<Users>()
                .Property(e => e.Role)
                .HasConversion<string>();

            modelBuilder.Entity<SpecialistDoctor>()
                .Property(e => e.Specialization)
                .HasConversion<string>();
            
            modelBuilder.Entity<Appointment>()
                .Property(e => e.AppointmentStatus)
                .HasConversion<string>();

            
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 5,
                    FirstName = "Archana",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "password123",
                    ContactNum = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = "Male",
                    Address = "123 Main St",
                    Pincode = "12345",
                    Role = RoleType.Doctor 
                }
            );

            modelBuilder.Entity<SpecialistDoctor>().HasData(
                new SpecialistDoctor
                {
                    Id = 2,
                    UserId = 5,
                    Specialization = "EyeSpecialist",
                }
                );
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    PatientId = 5, // Assuming user with Id 1 exists
                    ScheduleStartTime = DateTime.Now,
                    ScheduleEndTime = DateTime.Now.AddHours(1),
                    PatientProblem = "Some problem",
                    Description = "Some description",
                    AppointmentStatus = "Scheduled",
                    ConsultDoctor = "Dr. John Doe", // Assuming doctor exists with this name
                    NurseId = null // Assuming no nurse assigned initially
                }
                // Add more appointments as needed
            );
            //on cascade delete option
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }


        public DbSet<Users> Users { get; set; }
        public DbSet<SpecialistDoctor> SpecialistDoctors { get;set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}
