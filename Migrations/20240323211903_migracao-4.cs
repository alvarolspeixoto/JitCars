using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendaId",
                table: "Carros",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carros_VendaId",
                table: "Carros",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Vendas_VendaId",
                table: "Carros",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Vendas_VendaId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_VendaId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "VendaId",
                table: "Carros");
        }
    }
}
