using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCrmConsole.Migrations
{
    public partial class add_unique_index_VatNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    VatNumber = table.Column<string>(fixedLength: true, maxLength: 9, nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    InStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_VatNumber",
                schema: "core",
                table: "Customer",
                column: "VatNumber",
                unique: true,
                filter: "[VatNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "core");
        }
    }
}
