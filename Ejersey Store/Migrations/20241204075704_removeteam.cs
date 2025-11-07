using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jerseyShoppingCartMvcUI.Migrations
{
    /// <inheritdoc />
    public partial class removeteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "teamName",
                table: "jersey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "teamName",
                table: "jersey",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
