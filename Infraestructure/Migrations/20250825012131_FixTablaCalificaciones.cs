using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTablaCalificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_CursoEstudiante_CursoEstudianteId",
                table: "Calificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calificaciones",
                table: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_CursoEstudianteId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "CursoEstudianteId",
                table: "Calificaciones");

            migrationBuilder.RenameTable(
                name: "Calificaciones",
                newName: "Calificacion");

            migrationBuilder.AlterColumn<string>(
                name: "Concept",
                table: "Calificacion",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calificacion",
                table: "Calificacion",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_DateAdded",
                table: "Calificacion",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_IdCursoEstudiante",
                table: "Calificacion",
                column: "IdCursoEstudiante");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_CursoEstudiante_IdCursoEstudiante",
                table: "Calificacion",
                column: "IdCursoEstudiante",
                principalTable: "CursoEstudiante",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacion_CursoEstudiante_IdCursoEstudiante",
                table: "Calificacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calificacion",
                table: "Calificacion");

            migrationBuilder.DropIndex(
                name: "IX_Calificacion_DateAdded",
                table: "Calificacion");

            migrationBuilder.DropIndex(
                name: "IX_Calificacion_IdCursoEstudiante",
                table: "Calificacion");

            migrationBuilder.RenameTable(
                name: "Calificacion",
                newName: "Calificaciones");

            migrationBuilder.AlterColumn<string>(
                name: "Concept",
                table: "Calificaciones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CursoEstudianteId",
                table: "Calificaciones",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calificaciones",
                table: "Calificaciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_CursoEstudianteId",
                table: "Calificaciones",
                column: "CursoEstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_CursoEstudiante_CursoEstudianteId",
                table: "Calificaciones",
                column: "CursoEstudianteId",
                principalTable: "CursoEstudiante",
                principalColumn: "Id");
        }
    }
}
