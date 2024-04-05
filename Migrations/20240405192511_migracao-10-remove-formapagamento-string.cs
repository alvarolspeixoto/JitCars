using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao10removeformapagamentostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "Vendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormaPagamento",
                table: "Vendas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
