using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Reports_ReportId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_WashingMachines_WashingMachineId",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                schema: "21180047",
                table: "WashingMachines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "BusyUntil",
                schema: "21180047",
                table: "WashingMachines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VersionNo",
                schema: "21180047",
                table: "WashingMachines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionNo",
                schema: "21180047",
                table: "Statuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionNo",
                schema: "21180047",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                schema: "21180047",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsResolved",
                schema: "21180047",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VersionNo",
                schema: "21180047",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "WashingMachineId",
                schema: "21180047",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "VersionNo",
                schema: "21180047",
                table: "Conditions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionNo",
                schema: "21180047",
                table: "ActivityLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Buildings",
                schema: "21180047",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WashingMachines_BuildingId",
                schema: "21180047",
                table: "WashingMachines",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AuthorId",
                schema: "21180047",
                table: "Reports",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_WashingMachineId",
                schema: "21180047",
                table: "Reports",
                column: "WashingMachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "21180047",
                table: "ActivityLogs",
                column: "UserId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Reports_ReportId",
                schema: "21180047",
                table: "ActivityLogs",
                column: "ReportId",
                principalSchema: "21180047",
                principalTable: "Reports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports",
                column: "AuthorId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Reports_ReportId",
                schema: "21180047",
                table: "ActivityLogs");

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

            migrationBuilder.DropTable(
                name: "Buildings",
                schema: "21180047");

            migrationBuilder.DropIndex(
                name: "IX_WashingMachines_BuildingId",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropIndex(
                name: "IX_Reports_AuthorId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_WashingMachineId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "BusyUntil",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "VersionNo",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "VersionNo",
                schema: "21180047",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "VersionNo",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IsResolved",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "VersionNo",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "WashingMachineId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "VersionNo",
                schema: "21180047",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "VersionNo",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "21180047",
                table: "WashingMachines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "21180047",
                table: "WashingMachines",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "21180047",
                table: "WashingMachines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "21180047",
                table: "Reservations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "21180047",
                table: "Reports",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "21180047",
                table: "ActivityLogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "21180047",
                table: "ActivityLogs",
                column: "UserId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Reports_ReportId",
                schema: "21180047",
                table: "ActivityLogs",
                column: "ReportId",
                principalSchema: "21180047",
                principalTable: "Reports",
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
        }
    }
}
