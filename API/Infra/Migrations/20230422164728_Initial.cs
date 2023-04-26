using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ULTIMA_ATUALZIACAO_EM = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(70)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ULTIMA_ATUALZIACAO_EM = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    Age = table.Column<long>(type: "BIGINT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(600)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    OldOwnerId = table.Column<int>(type: "int", nullable: false),
                    NewOwnerId = table.Column<int>(type: "int", nullable: true),
                    CRIADO_EM = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ULTIMA_ATUALZIACAO_EM = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Users_NewOwnerId",
                        column: x => x.NewOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_Users_OldOwnerId",
                        column: x => x.OldOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    Bytes = table.Column<byte[]>(type: "BINARY(8000)", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ULTIMA_ATUALZIACAO_EM = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_id_active",
                table: "Addresses",
                columns: new[] { "Id", "ATIVO" });

            migrationBuilder.CreateIndex(
                name: "ix_id_active1",
                table: "Images",
                columns: new[] { "PetId", "ATIVO" });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_Id",
                table: "Pets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_NewOwnerId",
                table: "Pets",
                column: "NewOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OldOwnerId",
                table: "Pets",
                column: "OldOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
