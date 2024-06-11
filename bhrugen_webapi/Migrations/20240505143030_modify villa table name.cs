using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bhrugenwebapi.Migrations
{
    /// <inheritdoc />
    public partial class modifyvillatablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Villas",
                table: "Villas");

            migrationBuilder.RenameTable(
                name: "Villas",
                newName: "villatest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_villatest",
                table: "villatest",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "villatest",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 5, 17, 30, 30, 640, DateTimeKind.Local).AddTicks(8836));

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_villatest_VillaId",
                table: "VillaNumbers",
                column: "VillaId",
                principalTable: "villatest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_villatest_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_villatest",
                table: "villatest");

            migrationBuilder.RenameTable(
                name: "villatest",
                newName: "Villas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Villas",
                table: "Villas",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 15, 4, 37, 24, 241, DateTimeKind.Local).AddTicks(7462));

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
