using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class AddedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "WeekPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "SemesterCourseTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "SemesterAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "DepStuPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "DepartmentStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "DepartmentSemesterCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "WeekPrograms");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Attendances");
        }
    }
}
