using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eRestaurant.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SastojciProizvoda",
                newName: "SastojciProizvodaID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SastojciKategorije",
                newName: "SastojciKategorijeID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Sastojci",
                newName: "SastojakID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SanitarneDeratizacije",
                newName: "SanitarnaDeratizacijaID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Radnici",
                newName: "RadnikID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Pitanja",
                newName: "PitanjeID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Meniji",
                newName: "MeniID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Kuponi",
                newName: "KuponID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Inventari",
                newName: "InventarID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Dobavljaci",
                newName: "DobavljacID");

            migrationBuilder.AlterColumn<string>(
                name: "Slika",
                table: "Proizvodi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SastojciProizvodaID",
                table: "SastojciProizvoda",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SastojciKategorijeID",
                table: "SastojciKategorije",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SastojakID",
                table: "Sastojci",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SanitarnaDeratizacijaID",
                table: "SanitarneDeratizacije",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "RadnikID",
                table: "Radnici",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PitanjeID",
                table: "Pitanja",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "MeniID",
                table: "Meniji",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "KuponID",
                table: "Kuponi",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "InventarID",
                table: "Inventari",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "DobavljacID",
                table: "Dobavljaci",
                newName: "ID");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Slika",
                table: "Proizvodi",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
