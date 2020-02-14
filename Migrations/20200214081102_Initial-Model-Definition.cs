using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorialUniversity.Migrations
{
    public partial class InitialModelDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Credits = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "SomeVeryLongLongLongLongNameModel",
                columns: table => new
                {
                    MeaninglessColumnName2 = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeaninglessColumnName1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SomeVeryLongLongLongLongNameModel", x => x.MeaninglessColumnName2);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    EnrollmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: true),
                    VeryShort = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_SomeVeryLongLongLongLongNameModel_VeryShort",
                        column: x => x.VeryShort,
                        principalTable: "SomeVeryLongLongLongLongNameModel",
                        principalColumn: "MeaninglessColumnName2",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    PenaltyID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<int>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    Charge = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.PenaltyID);
                    table.ForeignKey(
                        name: "FK_Penalties_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseID",
                table: "Enrollments",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentID",
                table: "Enrollments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_VeryShort",
                table: "Enrollments",
                column: "VeryShort");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_StudentID",
                table: "Penalties",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "SomeVeryLongLongLongLongNameModel");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
