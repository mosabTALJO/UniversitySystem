using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class payments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepStuPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepStuId = table.Column<int>(type: "int", nullable: false),
                    SemAmntID = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepStuPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepStuPayments_DepartmentStudents_DepStuId",
                        column: x => x.DepStuId,
                        principalTable: "DepartmentStudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterAmounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemId = table.Column<int>(type: "int", nullable: false),
                    DepId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterAmounts_Departments_DepId",
                        column: x => x.DepId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterAmounts_Semesters_SemId",
                        column: x => x.SemId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepStuPayments_DepStuId",
                table: "DepStuPayments",
                column: "DepStuId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterAmounts_DepId",
                table: "SemesterAmounts",
                column: "DepId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterAmounts_SemId",
                table: "SemesterAmounts",
                column: "SemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepStuPayments");

            migrationBuilder.DropTable(
                name: "SemesterAmounts");
        }
    }
}
