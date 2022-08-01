using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class AddedSemesters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "DepartmentSemesterCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    GuzBaharYaz = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSemesterCourses_SemesterId",
                table: "DepartmentSemesterCourses",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSemesterCourses_Semesters_SemesterId",
                table: "DepartmentSemesterCourses",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSemesterCourses_Semesters_SemesterId",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSemesterCourses_SemesterId",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "DepartmentSemesterCourses");
        }
    }
}
