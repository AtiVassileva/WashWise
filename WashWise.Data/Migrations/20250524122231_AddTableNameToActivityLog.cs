using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableNameToActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TableName",
                schema: "21180047",
                table: "ActivityLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableName",
                schema: "21180047",
                table: "ActivityLogs");
        }
    }
}
