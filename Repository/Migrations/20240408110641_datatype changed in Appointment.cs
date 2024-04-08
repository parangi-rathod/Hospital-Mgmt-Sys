using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class datatypechangedinAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleStartTime",
                table: "Appointments",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleEndTime",
                table: "Appointments",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { new DateTime(2024, 4, 8, 17, 36, 41, 343, DateTimeKind.Local).AddTicks(5287), new DateTime(2024, 4, 8, 16, 36, 41, 343, DateTimeKind.Local).AddTicks(5275) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleStartTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleEndTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { new DateTime(2024, 4, 8, 17, 33, 24, 400, DateTimeKind.Local).AddTicks(3713), new DateTime(2024, 4, 8, 16, 33, 24, 400, DateTimeKind.Local).AddTicks(3699) });
        }
    }
}
