using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGGame.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Player identificator"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Player name"),
                    Health = table.Column<int>(type: "int", nullable: false, comment: "Player health"),
                    Mana = table.Column<int>(type: "int", nullable: false, comment: "Player mana"),
                    Damage = table.Column<int>(type: "int", nullable: false, comment: "Player damage"),
                    Strenght = table.Column<int>(type: "int", nullable: false, comment: "Player strenght"),
                    Agility = table.Column<int>(type: "int", nullable: false, comment: "Player agility"),
                    Intelligence = table.Column<int>(type: "int", nullable: false, comment: "Player intelligence"),
                    Range = table.Column<int>(type: "int", nullable: false, comment: "Player range"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Player date created")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                },
                comment: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
