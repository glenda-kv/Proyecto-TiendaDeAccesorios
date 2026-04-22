using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaAccesorios.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ModalidadPago",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "NumeroCuota",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "TotalCuotas",
                table: "Pago");

            migrationBuilder.AddColumn<string>(
                name: "ModalidadPago",
                table: "Venta",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalCuotas",
                table: "Venta",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Producto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Producto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModalidadPago",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "TotalCuotas",
                table: "Venta");

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Producto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Producto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Producto",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModalidadPago",
                table: "Pago",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroCuota",
                table: "Pago",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalCuotas",
                table: "Pago",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
