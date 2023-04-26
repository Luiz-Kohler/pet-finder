using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ULTIMA_ATUALZIACAO_EM",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CRIADO_EM",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ATIVO",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ULTIMA_ATUALZIACAO_EM",
                table: "Pets",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CRIADO_EM",
                table: "Pets",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Adopt Date",
                table: "Pets",
                newName: "AdoptDate");

            migrationBuilder.RenameColumn(
                name: "ATIVO",
                table: "Pets",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ULTIMA_ATUALZIACAO_EM",
                table: "Images",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CRIADO_EM",
                table: "Images",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ATIVO",
                table: "Images",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ULTIMA_ATUALZIACAO_EM",
                table: "Addresses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CRIADO_EM",
                table: "Addresses",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ATIVO",
                table: "Addresses",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "ULTIMA_ATUALZIACAO_EM");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "ATIVO");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CRIADO_EM");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Pets",
                newName: "ULTIMA_ATUALZIACAO_EM");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Pets",
                newName: "ATIVO");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Pets",
                newName: "CRIADO_EM");

            migrationBuilder.RenameColumn(
                name: "AdoptDate",
                table: "Pets",
                newName: "Adopt Date");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Images",
                newName: "ULTIMA_ATUALZIACAO_EM");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Images",
                newName: "ATIVO");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Images",
                newName: "CRIADO_EM");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Addresses",
                newName: "ULTIMA_ATUALZIACAO_EM");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Addresses",
                newName: "ATIVO");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Addresses",
                newName: "CRIADO_EM");
        }
    }
}
