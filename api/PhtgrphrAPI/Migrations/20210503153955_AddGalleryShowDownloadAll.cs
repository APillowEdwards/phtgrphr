using Microsoft.EntityFrameworkCore.Migrations;

namespace PhtgrphrAPI.Migrations
{
    public partial class AddGalleryShowDownloadAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowDownloadAll",
                table: "Galleries",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowDownloadAll",
                table: "Galleries");
        }
    }
}
