using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class asdsikl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenCourses_SemesterCourseTeachers_SemesterCourseTeacherId",
                table: "TakenCourses");

            migrationBuilder.DropIndex(
                name: "IX_TakenCourses_SemesterCourseTeacherId",
                table: "TakenCourses");

            migrationBuilder.DropColumn(
                name: "SemesterCourseTeacherId",
                table: "TakenCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterCourseTeacherId",
                table: "TakenCourses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TakenCourses_SemesterCourseTeacherId",
                table: "TakenCourses",
                column: "SemesterCourseTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenCourses_SemesterCourseTeachers_SemesterCourseTeacherId",
                table: "TakenCourses",
                column: "SemesterCourseTeacherId",
                principalTable: "SemesterCourseTeachers",
                principalColumn: "Id");
        }
    }
}
