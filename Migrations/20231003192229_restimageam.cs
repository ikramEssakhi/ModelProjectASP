using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelAsp1.Migrations
{
    /// <inheritdoc />
    public partial class restimageam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Product",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
