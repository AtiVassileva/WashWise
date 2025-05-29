using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_WashingMachines_Buildings_BuildingId",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports",
                column: "AuthorId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reports",
                column: "WashingMachineId",
                principalSchema: "21180047",
                principalTable: "WashingMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                schema: "21180047",
                table: "Reservations",
                column: "UserId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reservations",
                column: "WashingMachineId",
                principalSchema: "21180047",
                principalTable: "WashingMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WashingMachines_Buildings_BuildingId",
                schema: "21180047",
                table: "WashingMachines",
                column: "BuildingId",
                principalSchema: "21180047",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_WashingMachines_Buildings_BuildingId",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports",
                column: "AuthorId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reports",
                column: "WashingMachineId",
                principalSchema: "21180047",
                principalTable: "WashingMachines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                schema: "21180047",
                table: "Reservations",
                column: "UserId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reservations",
                column: "WashingMachineId",
                principalSchema: "21180047",
                principalTable: "WashingMachines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WashingMachines_Buildings_BuildingId",
                schema: "21180047",
                table: "WashingMachines",
                column: "BuildingId",
                principalSchema: "21180047",
                principalTable: "Buildings",
                principalColumn: "Id");
        }
    }
}
