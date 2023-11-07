using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasarYeriProject.DataAccessLayer.Migrations
{
    public partial class mig_add_product_img2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl2",
                table: "Products");
        }
    }
}
