using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class changedtostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { new DateTime(2024, 4, 8, 15, 50, 27, 945, DateTimeKind.Local).AddTicks(7594), new DateTime(2024, 4, 8, 14, 50, 27, 945, DateTimeKind.Local).AddTicks(7583) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ScheduleEndTime", "ScheduleStartTime" },
                values: new object[] { new DateTime(2024, 4, 5, 17, 20, 10, 766, DateTimeKind.Local).AddTicks(9903), new DateTime(2024, 4, 5, 16, 20, 10, 766, DateTimeKind.Local).AddTicks(9896) });
        }
    }
}
