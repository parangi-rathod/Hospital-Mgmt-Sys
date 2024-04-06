using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class stringconversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AppointmentStatus",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentStatus", "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { "Scheduled", new DateTime(2024, 4, 4, 17, 0, 24, 999, DateTimeKind.Local).AddTicks(7064), new DateTime(2024, 4, 4, 16, 0, 24, 999, DateTimeKind.Local).AddTicks(7056) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AppointmentStatus",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentStatus", "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { 0, new DateTime(2024, 4, 4, 16, 58, 44, 603, DateTimeKind.Local).AddTicks(6939), new DateTime(2024, 4, 4, 15, 58, 44, 603, DateTimeKind.Local).AddTicks(6926) });
        }
    }
}
