using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySystem.Migrations
{
    public partial class trackableShit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "WeekPrograms");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AddedByEmployeeId",
                table: "Attendances");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WeekPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WeekPrograms",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "WeekPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "WeekPrograms",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Teachers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Teachers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Teachers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TakenCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TakenCourses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TakenCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TakenCourses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Students",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Students",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Students",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Semesters",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Semesters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Semesters",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Semesters",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SemesterCourseTeachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SemesterCourseTeachers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SemesterCourseTeachers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "SemesterCourseTeachers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SemesterCourseTeachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SemesterCourseTeachers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SemesterAmounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SemesterAmounts",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SemesterAmounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "SemesterAmounts",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SemesterAmounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SemesterAmounts",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Salaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Salaries",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Salaries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Salaries",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Salaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Salaries",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ManagementStaffs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ManagementStaffs",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ManagementStaffs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ManagementStaffs",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ManagementStaffs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ManagementStaffs",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employees",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Employees",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Employees",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DepStuPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DepStuPayments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DepStuPayments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DepStuPayments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DepStuPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DepStuPayments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DepartmentStudents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DepartmentStudents",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DepartmentStudents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DepartmentStudents",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DepartmentStudents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DepartmentStudents",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DepartmentSemesterCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DepartmentSemesterCourses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DepartmentSemesterCourses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DepartmentSemesterCourses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DepartmentSemesterCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DepartmentSemesterCourses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Departments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Departments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Departments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Departments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Courses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Courses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Courses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Attendances",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Attendances",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WeekPrograms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WeekPrograms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WeekPrograms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "WeekPrograms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TakenCourses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TakenCourses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TakenCourses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TakenCourses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SemesterCourseTeachers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SemesterAmounts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ManagementStaffs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ManagementStaffs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ManagementStaffs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ManagementStaffs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ManagementStaffs");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ManagementStaffs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DepStuPayments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DepartmentStudents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DepartmentSemesterCourses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "WeekPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "SemesterCourseTeachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "SemesterAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "DepStuPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "DepartmentStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "DepartmentSemesterCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedByEmployeeId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
