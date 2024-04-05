using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao14adicionacolunafuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_AspNetUsers_FuncionarioId1",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_FuncionarioId1",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "FuncionarioId1",
                table: "Vendas");

            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "Vendas",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FuncionarioId",
                table: "Vendas",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_AspNetUsers_FuncionarioId",
                table: "Vendas",
                column: "FuncionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_AspNetUsers_FuncionarioId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_FuncionarioId",
                table: "Vendas");

            migrationBuilder.AlterColumn<Guid>(
                name: "FuncionarioId",
                table: "Vendas",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
