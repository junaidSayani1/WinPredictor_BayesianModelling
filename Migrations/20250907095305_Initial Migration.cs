using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dota2WinPredictor.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeroDTOs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Potrait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickCount = table.Column<int>(type: "int", nullable: true),
                    WinCount = table.Column<int>(type: "int", nullable: true),
                    WinRate = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroDTOs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroDTOs");
        }
    }
}
