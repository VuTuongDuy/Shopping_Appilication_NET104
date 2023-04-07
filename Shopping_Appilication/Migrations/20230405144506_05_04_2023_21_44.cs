using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping_Appilication.Migrations
{
    public partial class _05_04_2023_21_44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "User",
                type: "nvarchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "User");
        }
    }
}
