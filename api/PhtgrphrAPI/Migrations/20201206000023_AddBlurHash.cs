using Microsoft.EntityFrameworkCore.Migrations;

namespace PhtgrphrAPI.Migrations
{
    public partial class AddBlurHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlurHash",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlurHash",
                table: "Images");
        }
    }
}
