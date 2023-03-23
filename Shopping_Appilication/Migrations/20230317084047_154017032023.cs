using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping_Appilication.Migrations
{
    public partial class _154017032023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSole",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSize",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdColor",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Color_IdColor",
                table: "Product",
                column: "IdColor",
                principalTable: "Color",
                principalColumn: "IdColor");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Size_IdSize",
                table: "Product",
                column: "IdSize",
                principalTable: "Size",
                principalColumn: "IdSize");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Sole_IdSole",
                table: "Product",
                column: "IdSole",
                principalTable: "Sole",
                principalColumn: "IdSole");
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

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSole",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSize",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdColor",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
    }
}
