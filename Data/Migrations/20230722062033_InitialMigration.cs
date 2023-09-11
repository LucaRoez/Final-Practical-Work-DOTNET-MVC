using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinal___Bingo_Web_MVC_Service.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bingos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerNumber = table.Column<int>(type: "int", nullable: false),
                    Matrix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lottery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hits = table.Column<int>(type: "int", nullable: false),
                    WinningNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bingos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bingos");
        }
    }
}
