using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dota2WinPredictor.Migrations
{
    /// <inheritdoc />
    public partial class schemaupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AdjustedWinRate",
                table: "HeroDTOs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LogOdds",
                table: "HeroDTOs",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjustedWinRate",
                table: "HeroDTOs");

            migrationBuilder.DropColumn(
                name: "LogOdds",
                table: "HeroDTOs");
        }
    }
}
