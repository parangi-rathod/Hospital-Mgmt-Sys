using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class dataseedinginspecialistdoctortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SpecialistDoctors",
                columns: new[] { "Id", "Specialization", "UserId" },
                values: new object[] { 2, "EyeSpecialist", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SpecialistDoctors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
