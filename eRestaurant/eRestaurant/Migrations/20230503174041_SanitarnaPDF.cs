using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eRestaurant.Migrations
{
    public partial class SanitarnaPDF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PDF",
                table: "SanitarneDeratizacije",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PDF",
                table: "SanitarneDeratizacije");
        }
    }
}
