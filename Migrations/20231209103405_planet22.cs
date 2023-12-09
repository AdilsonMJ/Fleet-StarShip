using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetCommandAPI.Migrations
{
    /// <inheritdoc />
    public partial class planet22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_missions_planetModel_Planetid",
                table: "missions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_planetModel",
                table: "planetModel");

            migrationBuilder.RenameTable(
                name: "planetModel",
                newName: "planet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_planet",
                table: "planet",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_missions_planet_Planetid",
                table: "missions",
                column: "Planetid",
                principalTable: "planet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_missions_planet_Planetid",
                table: "missions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_planet",
                table: "planet");

            migrationBuilder.RenameTable(
                name: "planet",
                newName: "planetModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_planetModel",
                table: "planetModel",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_missions_planetModel_Planetid",
                table: "missions",
                column: "Planetid",
                principalTable: "planetModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
