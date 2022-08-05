using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class UserIdentity5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_DepartmentStudents_AspNetUsers_UserId",
            //    table: "DepartmentStudents");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Employees_AspNetUsers_UserId",
            //    table: "Employees");

            //migrationBuilder.DropIndex(
            //    name: "IX_Employees_UserId",
            //    table: "Employees");

            //migrationBuilder.DropIndex(
            //    name: "IX_DepartmentStudents_UserId",
            //    table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DepartmentStudents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DepartmentStudents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employees_UserId",
            //    table: "Employees",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DepartmentStudents_UserId",
            //    table: "DepartmentStudents",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_DepartmentStudents_AspNetUsers_UserId",
            //    table: "DepartmentStudents",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Employees_AspNetUsers_UserId",
            //    table: "Employees",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
