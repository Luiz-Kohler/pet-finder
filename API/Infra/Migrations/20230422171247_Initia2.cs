using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class Initia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Bytes",
                table: "Images",
                type: "VARBINARY",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(8000)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Bytes",
                table: "Images",
                type: "BINARY(8000)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY");
        }
    }
}
