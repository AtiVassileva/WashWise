using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLastModifiedOnColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "21180047",
                table: "WashingMachines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "WashingMachines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "21180047",
                table: "Statuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Statuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "21180047",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "21180047",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "21180047",
                table: "Conditions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Conditions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "21180047",
                table: "ActivityLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "ActivityLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "WashingMachines");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "21180047",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "21180047",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "Conditions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "ActivityLogs");
        }
    }
}
