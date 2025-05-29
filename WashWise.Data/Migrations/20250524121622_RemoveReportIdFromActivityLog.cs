using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReportIdFromActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Reports_ReportId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_ReportId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "ReportId",
                schema: "21180047",
                table: "ActivityLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports",
                column: "AuthorId",
                principalSchema: "21180047",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                schema: "21180047",
                table: "Reports");

            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                schema: "21180047",
                table: "ActivityLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ReportId",
                schema: "21180047",
                table: "ActivityLogs",
                column: "ReportId");

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
                onDelete: ReferentialAction.SetNull);
        }
    }
}
