using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_Estantes_EstanteId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_Publicaciones_PublicacionId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Estantes_Estanterias_EstanteriaId",
                table: "Estantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Publicaciones_PublicacionId1",
                table: "Libros");

            migrationBuilder.DropForeignKey(
                name: "FK_Revistas_Publicaciones_PublicacionId",
                table: "Revistas");

            migrationBuilder.DropIndex(
                name: "IX_Revistas_PublicacionId",
                table: "Revistas");

            migrationBuilder.DropIndex(
                name: "IX_Libros_PublicacionId1",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "SelectedAuthorIds",
                table: "Revistas");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Revistas");

            migrationBuilder.DropColumn(
                name: "PublicacionId1",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "SelectedAuthorIds",
                table: "Autores");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicacionId",
                table: "Revistas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EstanteriaId",
                table: "Estantes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicacionId",
                table: "Ejemplares",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EstanteId",
                table: "Ejemplares",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Revistas_PublicacionId",
                table: "Revistas",
                column: "PublicacionId",
                unique: true,
                filter: "[PublicacionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejemplares_Estantes_EstanteId",
                table: "Ejemplares",
                column: "EstanteId",
                principalTable: "Estantes",
                principalColumn: "EstanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejemplares_Publicaciones_PublicacionId",
                table: "Ejemplares",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "PublicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estantes_Estanterias_EstanteriaId",
                table: "Estantes",
                column: "EstanteriaId",
                principalTable: "Estanterias",
                principalColumn: "EstanteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Revistas_Publicaciones_PublicacionId",
                table: "Revistas",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "PublicacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_Estantes_EstanteId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_Publicaciones_PublicacionId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Estantes_Estanterias_EstanteriaId",
                table: "Estantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Revistas_Publicaciones_PublicacionId",
                table: "Revistas");

            migrationBuilder.DropIndex(
                name: "IX_Revistas_PublicacionId",
                table: "Revistas");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicacionId",
                table: "Revistas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedAuthorIds",
                table: "Revistas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Revistas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PublicacionId1",
                table: "Libros",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstanteriaId",
                table: "Estantes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicacionId",
                table: "Ejemplares",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstanteId",
                table: "Ejemplares",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedAuthorIds",
                table: "Autores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Revistas_PublicacionId",
                table: "Revistas",
                column: "PublicacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_PublicacionId1",
                table: "Libros",
                column: "PublicacionId1",
                unique: true,
                filter: "[PublicacionId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejemplares_Estantes_EstanteId",
                table: "Ejemplares",
                column: "EstanteId",
                principalTable: "Estantes",
                principalColumn: "EstanteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ejemplares_Publicaciones_PublicacionId",
                table: "Ejemplares",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "PublicacionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estantes_Estanterias_EstanteriaId",
                table: "Estantes",
                column: "EstanteriaId",
                principalTable: "Estanterias",
                principalColumn: "EstanteriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Publicaciones_PublicacionId1",
                table: "Libros",
                column: "PublicacionId1",
                principalTable: "Publicaciones",
                principalColumn: "PublicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Revistas_Publicaciones_PublicacionId",
                table: "Revistas",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "PublicacionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
