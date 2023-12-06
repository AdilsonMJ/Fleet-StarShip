using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetCommandAPI.Migrations
{
    /// <inheritdoc />
    public partial class planets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Planet",
                table: "missions");

            migrationBuilder.AddColumn<int>(
                name: "Planetid",
                table: "missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "planetModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Population = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Terrain = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planetModel", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_missions_Planetid",
                table: "missions",
                column: "Planetid");

            migrationBuilder.AddForeignKey(
                name: "FK_missions_planetModel_Planetid",
                table: "missions",
                column: "Planetid",
                principalTable: "planetModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_missions_planetModel_Planetid",
                table: "missions");

            migrationBuilder.DropTable(
                name: "planetModel");

            migrationBuilder.DropIndex(
                name: "IX_missions_Planetid",
                table: "missions");

            migrationBuilder.DropColumn(
                name: "Planetid",
                table: "missions");

            migrationBuilder.AddColumn<string>(
                name: "Planet",
                table: "missions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
