using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangFire.Infrastructure.Migrations
{
    public partial class AddEntityMtgCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MtgCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OracleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScryfallUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    manaCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertedManaCost = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OracleText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Power = table.Column<int>(type: "int", nullable: true),
                    Toughness = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EdhrecRank = table.Column<int>(type: "int", nullable: false),
                    PennyRank = table.Column<int>(type: "int", nullable: false),
                    ProducesMana = table.Column<bool>(type: "bit", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MtgCards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MtgCards");
        }
    }
}
