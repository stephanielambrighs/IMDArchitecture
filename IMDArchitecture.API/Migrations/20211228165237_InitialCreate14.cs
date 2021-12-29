using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDArchitecture.API.Migrations
{
    public partial class InitialCreate14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetAge",
                table: "Events",
                newName: "MinAge");

            migrationBuilder.AddColumn<int>(
                name: "MaxAge",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxAge",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "MinAge",
                table: "Events",
                newName: "TargetAge");
        }
    }
}
