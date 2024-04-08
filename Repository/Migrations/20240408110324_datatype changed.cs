using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class datatypechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { new DateTime(2024, 4, 8, 17, 33, 24, 400, DateTimeKind.Local).AddTicks(3713), new DateTime(2024, 4, 8, 16, 33, 24, 400, DateTimeKind.Local).AddTicks(3699) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { new DateTime(2024, 4, 8, 15, 50, 27, 945, DateTimeKind.Local).AddTicks(7594), new DateTime(2024, 4, 8, 14, 50, 27, 945, DateTimeKind.Local).AddTicks(7583) });
        }
    }
}
