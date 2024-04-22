using Microsoft.EntityFrameworkCore;
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
            
            modelBuilder.Entity<SpecialistDoctor>().HasData(
                new SpecialistDoctor
                {
                    Id = 2,
                    UserId = 1,
                    Specialization = "EyeSpecialist",
                }
                );

            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.Nurse)   
            //    .WithMany(u => u.Nurse)             
            //    .HasForeignKey(a => a.NurseId)  
            //    .IsRequired(false);

            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.ConsultDoctor)
            //    .WithMany(u=>u.Doctor)
            //    .HasForeignKey(a => a.ConsultDoctorId)
            //    .IsRequired(false);

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    FirstName = "Archana",
                    LastName = "Vyas",
                    Email = "archana.vyas@bacancy.com",
                    Password = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f",
                    ContactNum = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = "Female",
                    Address = "123 Main St",
                    Pincode = "123456",
                    Role = RoleType.Doctor
                }
            );

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
