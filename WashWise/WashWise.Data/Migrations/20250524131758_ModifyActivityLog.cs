using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "VersionNo",
                schema: "21180047",
                table: "ActivityLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "21180047",
                table: "ActivityLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VersionNo",
                schema: "21180047",
                table: "ActivityLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                schema: "21180047",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                schema: "21180047",
                table: "ActivityLogs",
                column: "UserId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
