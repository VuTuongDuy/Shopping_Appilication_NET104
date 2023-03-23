using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping_Appilication.Migrations
{
    public partial class AddThuocTinhSP_16_03_2023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Role");

            migrationBuilder.AddColumn<Guid>(
                name: "IdColor",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdSize",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdSole",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    IdColor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.IdColor);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    IdSize = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.IdSize);
                });

            migrationBuilder.CreateTable(
                name: "Sole",
                columns: table => new
                {
                    IdSole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Fabric = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sole", x => x.IdSole);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdColor",
                table: "Product",
                column: "IdColor");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdSize",
                table: "Product",
                column: "IdSize");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdSole",
                table: "Product",
                column: "IdSole");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Color_IdColor",
                table: "Product",
                column: "IdColor",
                principalTable: "Color",
                principalColumn: "IdColor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Size_IdSize",
                table: "Product",
                column: "IdSize",
                principalTable: "Size",
                principalColumn: "IdSize",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Sole_IdSole",
                table: "Product",
                column: "IdSole",
                principalTable: "Sole",
                principalColumn: "IdSole",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Color_IdColor",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Size_IdSize",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Sole_IdSole",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Sole");

            migrationBuilder.DropIndex(
                name: "IX_Product_IdColor",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_IdSize",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_IdSole",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdColor",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdSize",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdSole",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
