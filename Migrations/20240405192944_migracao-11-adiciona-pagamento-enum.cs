using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao11adicionapagamentoenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormaPagamento",
                table: "Vendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "Vendas");
        }
    }
}
