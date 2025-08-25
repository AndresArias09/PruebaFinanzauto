using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnasUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_NombreUsuario",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "NombreUsuario",
                table: "Usuario",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuario",
                newName: "Password");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UserName",
                table: "Usuario",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_UserName",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Usuario",
                newName: "NombreUsuario");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuario",
                newName: "Contraseña");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NombreUsuario",
                table: "Usuario",
                column: "NombreUsuario");
        }
    }
}
