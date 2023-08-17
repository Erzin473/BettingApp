using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BettingWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addOddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odd_Bets_BetID",
                table: "Odd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Odd",
                table: "Odd");

            migrationBuilder.RenameTable(
                name: "Odd",
                newName: "Odds");

            migrationBuilder.RenameIndex(
                name: "IX_Odd_BetID",
                table: "Odds",
                newName: "IX_Odds_BetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Odds",
                table: "Odds",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Odds_Bets_BetID",
                table: "Odds",
                column: "BetID",
                principalTable: "Bets",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odds_Bets_BetID",
                table: "Odds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Odds",
                table: "Odds");

            migrationBuilder.RenameTable(
                name: "Odds",
                newName: "Odd");

            migrationBuilder.RenameIndex(
                name: "IX_Odds_BetID",
                table: "Odd",
                newName: "IX_Odd_BetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Odd",
                table: "Odd",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Odd_Bets_BetID",
                table: "Odd",
                column: "BetID",
                principalTable: "Bets",
                principalColumn: "ID");
        }
    }
}
