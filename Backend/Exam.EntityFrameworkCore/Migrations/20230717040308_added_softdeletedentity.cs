using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class addedsoftdeletedentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "main",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "main",
                table: "Tasks");
        }
    }
}
