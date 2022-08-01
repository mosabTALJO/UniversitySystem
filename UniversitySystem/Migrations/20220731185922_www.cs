using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class www : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagementStaffs_Salaries_SalaryId",
                table: "ManagementStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Salaries_SalaryId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SalaryId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_ManagementStaffs_SalaryId",
                table: "ManagementStaffs");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "ManagementStaffs");

            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "TakenCourses",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "TakenCourses",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryId",
                table: "ManagementStaffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SalaryId",
                table: "Teachers",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagementStaffs_SalaryId",
                table: "ManagementStaffs",
                column: "SalaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagementStaffs_Salaries_SalaryId",
                table: "ManagementStaffs",
                column: "SalaryId",
                principalTable: "Salaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Salaries_SalaryId",
                table: "Teachers",
                column: "SalaryId",
                principalTable: "Salaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
