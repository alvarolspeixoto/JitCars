using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao13adicionacolunafuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FuncionarioId",
                table: "Vendas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FuncionarioId1",
                table: "Vendas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FuncionarioId1",
                table: "Vendas",
                column: "FuncionarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_AspNetUsers_FuncionarioId1",
                table: "Vendas",
                column: "FuncionarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_AspNetUsers_FuncionarioId1",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_FuncionarioId1",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "FuncionarioId1",
                table: "Vendas");
        }
    }
}
