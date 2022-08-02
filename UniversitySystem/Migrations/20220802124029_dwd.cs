using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class dwd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentsAmount",
                table: "DepartmentSemesterCourses",
                newName: "NumberOfStudents");

            migrationBuilder.AddColumn<int>(
                name: "MaxStudents",
                table: "DepartmentSemesterCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxStudents",
                table: "DepartmentSemesterCourses");

            migrationBuilder.RenameColumn(
                name: "NumberOfStudents",
                table: "DepartmentSemesterCourses",
                newName: "StudentsAmount");
        }
    }
}
