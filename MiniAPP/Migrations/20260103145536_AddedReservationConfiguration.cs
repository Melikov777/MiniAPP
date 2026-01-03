using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniAPP.Migrations
{
    /// <inheritdoc />
    public partial class AddedReservationConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Restaurant_RestaurantId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RestaurantId",
                table: "Reservation",
                newName: "IX_Reservation_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DiningTableId",
                table: "Reservation",
                newName: "IX_Reservation_DiningTableId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Reservation",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Reservation_GuestCount",
                table: "Reservation",
                sql: "\"GuestCount\" >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Reservation_ReservationDate",
                table: "Reservation",
                sql: "\"ReservationDate\" >= CAST(NOW() AS DATE)");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_DiningTable_DiningTableId",
                table: "Reservation",
                column: "DiningTableId",
                principalTable: "DiningTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Restaurant_RestaurantId",
                table: "Reservation",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_DiningTable_DiningTableId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Restaurant_RestaurantId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Reservation_GuestCount",
                table: "Reservation");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Reservation_ReservationDate",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_RestaurantId",
                table: "Reservations",
                newName: "IX_Reservations_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_DiningTableId",
                table: "Reservations",
                newName: "IX_Reservations_DiningTableId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_DiningTable_DiningTableId",
                table: "Reservations",
                column: "DiningTableId",
                principalTable: "DiningTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Restaurant_RestaurantId",
                table: "Reservations",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
