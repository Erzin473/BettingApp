using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BettingWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addBetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLive = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Odd",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialBetValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odd", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odd_Bets_BetID",
                        column: x => x.BetID,
                        principalTable: "Bets",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odd_BetID",
                table: "Odd",
                column: "BetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odd");

            migrationBuilder.DropTable(
                name: "Bets");
        }
    }
}
