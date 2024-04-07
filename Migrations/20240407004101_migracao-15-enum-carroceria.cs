using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JitCars.Migrations
{
    /// <inheritdoc />
    public partial class migracao15enumcarroceria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carroceria",
                table: "Modelos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Carroceria",
                table: "Modelos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
