using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFabricacao",
                table: "Modelos");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Modelos",
                newName: "Carroceria");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Enderecos",
                newName: "Cep");

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Telefones",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FuncionarioId1",
                table: "Telefones",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnoFabricacao",
                table: "Modelos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PrimeiroNome",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Salario = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_FuncionarioId1",
                table: "Telefones",
                column: "FuncionarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CargoId",
                table: "AspNetUsers",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EnderecoId",
                table: "AspNetUsers",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cargo_CargoId",
                table: "AspNetUsers",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Enderecos_EnderecoId",
                table: "AspNetUsers",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_AspNetUsers_FuncionarioId1",
                table: "Telefones",
                column: "FuncionarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cargo_CargoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Enderecos_EnderecoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_AspNetUsers_FuncionarioId1",
                table: "Telefones");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropIndex(
                name: "IX_Telefones_FuncionarioId1",
                table: "Telefones");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CargoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EnderecoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Telefones");

            migrationBuilder.DropColumn(
                name: "FuncionarioId1",
                table: "Telefones");

            migrationBuilder.DropColumn(
                name: "AnoFabricacao",
                table: "Modelos");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrimeiroNome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Carroceria",
                table: "Modelos",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Enderecos",
                newName: "CEP");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFabricacao",
                table: "Modelos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
