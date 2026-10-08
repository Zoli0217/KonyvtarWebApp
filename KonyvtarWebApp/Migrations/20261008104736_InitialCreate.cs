using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonyvtarWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Szerzok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nev = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SzuletesiEv = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szerzok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konyvek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cim = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    KiadasEve = table.Column<int>(type: "INTEGER", nullable: false),
                    SzerzoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konyvek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konyvek_Szerzok_SzerzoId",
                        column: x => x.SzerzoId,
                        principalTable: "Szerzok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konyvek_SzerzoId",
                table: "Konyvek",
                column: "SzerzoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konyvek");

            migrationBuilder.DropTable(
                name: "Szerzok");
        }
    }
}
