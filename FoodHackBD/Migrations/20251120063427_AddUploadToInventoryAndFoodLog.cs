using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodHackBD.Migrations
{
    /// <inheritdoc />
    public partial class AddUploadToInventoryAndFoodLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UploadId",
                table: "Inventories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UploadId",
                table: "FoodLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_UploadId",
                table: "Inventories",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodLogs_UploadId",
                table: "FoodLogs",
                column: "UploadId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodLogs_Uploads_UploadId",
                table: "FoodLogs",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Uploads_UploadId",
                table: "Inventories",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodLogs_Uploads_UploadId",
                table: "FoodLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Uploads_UploadId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_UploadId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_FoodLogs_UploadId",
                table: "FoodLogs");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "FoodLogs");
        }
    }
}
