using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class initialSanti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Publicaciones_PublicacionId",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_PublicacionId",
                table: "Libros");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicacionId",
                table: "Libros",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_PublicacionId",
                table: "Libros",
                column: "PublicacionId",
                unique: true,
                filter: "[PublicacionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Publicaciones_PublicacionId",
                table: "Libros",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "PublicacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Publicaciones_PublicacionId",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_PublicacionId",
                table: "Libros");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicacionId",
                table: "Libros",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_PublicacionId",
                table: "Libros",
                column: "PublicacionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Publicaciones_PublicacionId",
                table: "Libros",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "PublicacionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
