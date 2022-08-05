using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class UserIdentity3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DepartmentStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "DepartmentStudents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId1",
                table: "Employees",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentStudents_UserId1",
                table: "DepartmentStudents",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentStudents_AspNetUsers_UserId1",
                table: "DepartmentStudents",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_UserId1",
                table: "Employees",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentStudents_AspNetUsers_UserId1",
                table: "DepartmentStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_UserId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentStudents_UserId1",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "DepartmentStudents");
        }
    }
}
