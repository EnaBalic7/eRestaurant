using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eRestaurant.Migrations
{
    public partial class KolicinaSastojka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KolicinaNaStanju",
                table: "Sastojci",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KolicinaNaStanju",
                table: "Sastojci");
        }
    }
}
