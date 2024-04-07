using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao16enumcarroceria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Carroceria",
                table: "Modelos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carroceria",
                table: "Modelos");
        }
    }
}
