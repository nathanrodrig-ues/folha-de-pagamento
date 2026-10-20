using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nathanrodrigues.Migrations
{
    /// <inheritdoc />
    public partial class InicialProva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Fgts",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ImpostoRenda",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Inss",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalarioBruto",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalarioLiquido",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fgts",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "ImpostoRenda",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "Inss",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "SalarioBruto",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "SalarioLiquido",
                table: "Folhas");
        }
    }
}
