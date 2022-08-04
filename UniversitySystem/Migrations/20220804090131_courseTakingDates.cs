using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class courseTakingDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CourseTakingEndDate",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CourseTakingStartDate",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPayed",
            //    table: "DepStuPayments",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<double>(
            //    name: "Balance",
            //    table: "DepartmentStudents",
            //    type: "float",
            //    nullable: false,
            //    defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTakingEndDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "CourseTakingStartDate",
                table: "Semesters");

            //migrationBuilder.DropColumn(
            //    name: "IsPayed",
            //    table: "DepStuPayments");

            //migrationBuilder.DropColumn(
            //    name: "Balance",
            //    table: "DepartmentStudents");
        }
    }
}
