using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class fixedTakenCourseTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenCourses_Teachers_TeacherId",
                table: "TakenCourses");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "TakenCourses",
                newName: "SemCourseTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TakenCourses_TeacherId",
                table: "TakenCourses",
                newName: "IX_TakenCourses_SemCourseTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenCourses_SemesterCourseTeachers_SemCourseTeacherId",
                table: "TakenCourses",
                column: "SemCourseTeacherId",
                principalTable: "SemesterCourseTeachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenCourses_SemesterCourseTeachers_SemCourseTeacherId",
                table: "TakenCourses");

            migrationBuilder.RenameColumn(
                name: "SemCourseTeacherId",
                table: "TakenCourses",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TakenCourses_SemCourseTeacherId",
                table: "TakenCourses",
                newName: "IX_TakenCourses_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenCourses_Teachers_TeacherId",
                table: "TakenCourses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
