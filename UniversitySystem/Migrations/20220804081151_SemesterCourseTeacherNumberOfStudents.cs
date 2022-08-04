using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class SemesterCourseTeacherNumberOfStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfStudents",
                table: "SemesterCourseTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfStudents",
                table: "SemesterCourseTeachers");
        }
    }
}
