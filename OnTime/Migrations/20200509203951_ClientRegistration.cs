using Microsoft.EntityFrameworkCore.Migrations;

namespace OnTime.Migrations
{
    public partial class ClientRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientPersonal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    ShipmentsPerWeek = table.Column<int>(nullable: false),
                    CnicNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: false),
                    WebsiteUrl = table.Column<string>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPersonal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPersonal_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientPersonal_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Cosmetics" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Appliances" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientPersonal_ProductTypeId",
                table: "ClientPersonal",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPersonal_UserId",
                table: "ClientPersonal",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientPersonal");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
