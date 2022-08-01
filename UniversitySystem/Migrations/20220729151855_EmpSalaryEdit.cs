using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class EmpSalaryEdit : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                table: "Salaries",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Salaries");

            migrationBuilder.AddColumn<int>(
                name: "SalaryId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
