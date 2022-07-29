using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class asdasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_DepartmentSemesterCourses_CourseId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCourseTeachers_DepartmentSemesterCourses_CourseId",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenCourses_DepartmentSemesterCourses_CourseId",
                table: "TakenCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_WeekPrograms_DepartmentSemesterCourses_CourseId",
                table: "WeekPrograms");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "WeekPrograms",
                newName: "DepSemCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_WeekPrograms_CourseId",
                table: "WeekPrograms",
                newName: "IX_WeekPrograms_DepSemCourseId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "TakenCourses",
                newName: "DepSemCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_TakenCourses_CourseId",
                table: "TakenCourses",
                newName: "IX_TakenCourses_DepSemCourseId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "SemesterCourseTeachers",
                newName: "DepSemCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterCourseTeachers_CourseId",
                table: "SemesterCourseTeachers",
                newName: "IX_SemesterCourseTeachers_DepSemCourseId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Attendances",
                newName: "DepSemCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_CourseId",
                table: "Attendances",
                newName: "IX_Attendances_DepSemCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_DepartmentSemesterCourses_DepSemCourseId",
                table: "Attendances",
                column: "DepSemCourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCourseTeachers_DepartmentSemesterCourses_DepSemCourseId",
                table: "SemesterCourseTeachers",
                column: "DepSemCourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenCourses_DepartmentSemesterCourses_DepSemCourseId",
                table: "TakenCourses",
                column: "DepSemCourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeekPrograms_DepartmentSemesterCourses_DepSemCourseId",
                table: "WeekPrograms",
                column: "DepSemCourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_DepartmentSemesterCourses_DepSemCourseId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterCourseTeachers_DepartmentSemesterCourses_DepSemCourseId",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenCourses_DepartmentSemesterCourses_DepSemCourseId",
                table: "TakenCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_WeekPrograms_DepartmentSemesterCourses_DepSemCourseId",
                table: "WeekPrograms");

            migrationBuilder.RenameColumn(
                name: "DepSemCourseId",
                table: "WeekPrograms",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_WeekPrograms_DepSemCourseId",
                table: "WeekPrograms",
                newName: "IX_WeekPrograms_CourseId");

            migrationBuilder.RenameColumn(
                name: "DepSemCourseId",
                table: "TakenCourses",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_TakenCourses_DepSemCourseId",
                table: "TakenCourses",
                newName: "IX_TakenCourses_CourseId");

            migrationBuilder.RenameColumn(
                name: "DepSemCourseId",
                table: "SemesterCourseTeachers",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterCourseTeachers_DepSemCourseId",
                table: "SemesterCourseTeachers",
                newName: "IX_SemesterCourseTeachers_CourseId");

            migrationBuilder.RenameColumn(
                name: "DepSemCourseId",
                table: "Attendances",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_DepSemCourseId",
                table: "Attendances",
                newName: "IX_Attendances_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_DepartmentSemesterCourses_CourseId",
                table: "Attendances",
                column: "CourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterCourseTeachers_DepartmentSemesterCourses_CourseId",
                table: "SemesterCourseTeachers",
                column: "CourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenCourses_DepartmentSemesterCourses_CourseId",
                table: "TakenCourses",
                column: "CourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeekPrograms_DepartmentSemesterCourses_CourseId",
                table: "WeekPrograms",
                column: "CourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
