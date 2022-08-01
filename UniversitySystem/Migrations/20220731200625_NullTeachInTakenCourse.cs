using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class NullTeachInTakenCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenCourses_Teachers_TeacherId",
                table: "TakenCourses");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "TakenCourses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenCourses_Teachers_TeacherId",
                table: "TakenCourses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenCourses_Teachers_TeacherId",
                table: "TakenCourses");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "TakenCourses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenCourses_Teachers_TeacherId",
                table: "TakenCourses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
