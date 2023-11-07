using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasarYeriProject.DataAccessLayer.Migrations
{
    public partial class add_mig_prop_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProductStatus",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UserStatus",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "AspNetUsers");
        }
    }
}
