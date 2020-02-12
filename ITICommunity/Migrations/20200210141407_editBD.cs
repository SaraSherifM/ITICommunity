using Microsoft.EntityFrameworkCore.Migrations;

namespace ITICommunity.Migrations
{
    public partial class editBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirtghDay",
                table: "User",
                newName: "BirthDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "User",
                newName: "BirtghDay");
        }
    }
}
