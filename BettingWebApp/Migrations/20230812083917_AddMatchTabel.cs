using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BettingWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Match_MatchID",
                table: "Bets");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Events_EventID",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Matchs");

            migrationBuilder.RenameIndex(
                name: "IX_Match_EventID",
                table: "Matchs",
                newName: "IX_Matchs_EventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matchs",
                table: "Matchs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Matchs_MatchID",
                table: "Bets",
                column: "MatchID",
                principalTable: "Matchs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Events_EventID",
                table: "Matchs",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Matchs_MatchID",
                table: "Bets");

            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Events_EventID",
                table: "Matchs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matchs",
                table: "Matchs");

            migrationBuilder.RenameTable(
                name: "Matchs",
                newName: "Match");

            migrationBuilder.RenameIndex(
                name: "IX_Matchs_EventID",
                table: "Match",
                newName: "IX_Match_EventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Match_MatchID",
                table: "Bets",
                column: "MatchID",
                principalTable: "Match",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Events_EventID",
                table: "Match",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID");
        }
    }
}
