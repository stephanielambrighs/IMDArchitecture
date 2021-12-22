using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDArchitecture.API.Migrations
{
    public partial class InitialCreate7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Participants",
                table: "Events",
                newName: "ParticipantCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParticipantCount",
                table: "Events",
                newName: "Participants");
        }
    }
}
