using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class edits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_DepartmentSemesterCourses_DepSemCourseId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_DepartmentStudents_DepartmentStudentId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_DepartmentStudentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_DepSemCourseId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DepSemCourseId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DepartmentStudentId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Attendances",
                newName: "TakenCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                newName: "IX_Attendances_TakenCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_TakenCourses_TakenCourseId",
                table: "Attendances",
                column: "TakenCourseId",
                principalTable: "TakenCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_TakenCourses_TakenCourseId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "TakenCourseId",
                table: "Attendances",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_TakenCourseId",
                table: "Attendances",
                newName: "IX_Attendances_StudentId");

            migrationBuilder.AddColumn<int>(
                name: "DepSemCourseId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentStudentId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DepartmentStudentId",
                table: "Attendances",
                column: "DepartmentStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DepSemCourseId",
                table: "Attendances",
                column: "DepSemCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_DepartmentSemesterCourses_DepSemCourseId",
                table: "Attendances",
                column: "DepSemCourseId",
                principalTable: "DepartmentSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_DepartmentStudents_DepartmentStudentId",
                table: "Attendances",
                column: "DepartmentStudentId",
                principalTable: "DepartmentStudents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
