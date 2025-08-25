using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surnames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surnames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdProfesor = table.Column<long>(type: "bigint", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_Profesor_IdProfesor",
                        column: x => x.IdProfesor,
                        principalTable: "Profesor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CursoEstudiante",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEstudiante = table.Column<long>(type: "bigint", nullable: true),
                    IdCurso = table.Column<long>(type: "bigint", nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoEstudiante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursoEstudiante_Curso_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CursoEstudiante_Estudiante_IdEstudiante",
                        column: x => x.IdEstudiante,
                        principalTable: "Estudiante",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCursoEstudiante = table.Column<long>(type: "bigint", nullable: true),
                    CursoEstudianteId = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Concept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_CursoEstudiante_CursoEstudianteId",
                        column: x => x.CursoEstudianteId,
                        principalTable: "CursoEstudiante",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Estudiante",
                columns: new[] { "Id", "DateAdded", "DateLastUpdate", "DocumentId", "Email", "EntryDate", "Names", "Surnames" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "99888923", "example@domain.co", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Local), "Andrés Leonardo", "Arias Uribe" },
                    { 2L, new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "998889223", "example@domain.co", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Local), "David Alfonso", "Cárdenas Suarez" }
                });

            migrationBuilder.InsertData(
                table: "Profesor",
                columns: new[] { "Id", "DateAdded", "DateLastUpdate", "DocumentId", "Email", "EntryDate", "Names", "Surnames" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "77882323", "example@domain.co", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Local), "Maria", "Chavez" },
                    { 2L, new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "1237676", "example@domain.co", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Local), "Henry Andres", "Hernandez Hernandez" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Contraseña", "DateAdded", "DateLastUpdate", "NombreUsuario" },
                values: new object[] { 1L, "B9A465912169BEF97138C76EFDFD5BB34FDC5FA58855AC187817AE07E80ABE5E-5929B1B6239B2767DDEDDABC98823ADF", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_CursoEstudianteId",
                table: "Calificaciones",
                column: "CursoEstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_IdProfesor",
                table: "Curso",
                column: "IdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_StartDate",
                table: "Curso",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_CursoEstudiante_DateAdded",
                table: "CursoEstudiante",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_CursoEstudiante_EnrollmentDate",
                table: "CursoEstudiante",
                column: "EnrollmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_CursoEstudiante_IdCurso",
                table: "CursoEstudiante",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_CursoEstudiante_IdEstudiante",
                table: "CursoEstudiante",
                column: "IdEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_DateAdded",
                table: "Estudiante",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_DocumentId",
                table: "Estudiante",
                column: "DocumentId",
                unique: true,
                filter: "[DocumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_EntryDate",
                table: "Estudiante",
                column: "EntryDate");

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_DateAdded",
                table: "Profesor",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_DocumentId",
                table: "Profesor",
                column: "DocumentId",
                unique: true,
                filter: "[DocumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profesor_EntryDate",
                table: "Profesor",
                column: "EntryDate");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DateAdded",
                table: "Usuario",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NombreUsuario",
                table: "Usuario",
                column: "NombreUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "CursoEstudiante");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Profesor");
        }
    }
}
