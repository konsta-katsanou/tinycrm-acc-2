using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCrmConsole.Migrations
{
    public partial class newproductproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InStock",
                schema: "core",
                table: "Product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                schema: "core",
                table: "Product");
        }
    }
}
