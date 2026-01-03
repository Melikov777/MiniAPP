using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiningTableConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiningTables_Restaurant_RestaurantId",
                table: "DiningTables");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_DiningTables_DiningTableId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiningTables",
                table: "DiningTables");

            migrationBuilder.DropIndex(
                name: "IX_DiningTables_RestaurantId",
                table: "DiningTables");

            migrationBuilder.RenameTable(
                name: "DiningTables",
                newName: "DiningTable");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "DiningTable",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiningTable",
                table: "DiningTable",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DiningTable_RestaurantId_DiningTableNumber",
                table: "DiningTable",
                columns: new[] { "RestaurantId", "DiningTableNumber" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_DiningTable_Capacity",
                table: "DiningTable",
                sql: "\"SeatingCapacity\" >= 1");

            migrationBuilder.AddForeignKey(
                name: "FK_DiningTable_Restaurant_RestaurantId",
                table: "DiningTable",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations",
                column: "DiningTableId",
                principalTable: "DiningTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiningTable_Restaurant_RestaurantId",
                table: "DiningTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiningTable",
                table: "DiningTable");

            migrationBuilder.DropIndex(
                name: "IX_DiningTable_RestaurantId_DiningTableNumber",
                table: "DiningTable");

            migrationBuilder.DropCheckConstraint(
                name: "CK_DiningTable_Capacity",
                table: "DiningTable");

            migrationBuilder.RenameTable(
                name: "DiningTable",
                newName: "DiningTables");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "DiningTables",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiningTables",
                table: "DiningTables",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DiningTables_RestaurantId",
                table: "DiningTables",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiningTables_Restaurant_RestaurantId",
                table: "DiningTables",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_DiningTables_DiningTableId",
                table: "Reservations",
                column: "DiningTableId",
                principalTable: "DiningTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
