using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetTenderService.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToTenders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Tenders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Tenders");
        }
    }
}
