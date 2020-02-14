using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorialUniversity.Migrations
{
    public partial class AddSomeSecrettoStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SomeSecret",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SomeSecret",
                table: "Students");
        }
    }
}
