using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping_Appilication.Migrations
{
    public partial class add_table_image_19032023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdImage",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    IdImage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Image3 = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Image4 = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.IdImage);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdImage",
                table: "Product",
                column: "IdImage");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Image_IdImage",
                table: "Product",
                column: "IdImage",
                principalTable: "Image",
                principalColumn: "IdImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Image_IdImage",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Product_IdImage",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdImage",
                table: "Product");
        }
    }
}
