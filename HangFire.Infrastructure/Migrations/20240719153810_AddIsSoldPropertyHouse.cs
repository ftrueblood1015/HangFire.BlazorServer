using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangFire.Infrastructure.Migrations
{
    public partial class AddIsSoldPropertyHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Houses");
        }
    }
}
