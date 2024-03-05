using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEventos.Persistence.Migrations
{
    public partial class UpdateIdentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_AspNetUsers_UserId1",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Palestrantes_AspNetUsers_UserId1",
                table: "Palestrantes");

            migrationBuilder.DropIndex(
                name: "IX_Palestrantes_UserId1",
                table: "Palestrantes");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_UserId1",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Palestrantes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Eventos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Palestrantes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Eventos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Palestrantes_UserId",
                table: "Palestrantes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_UserId",
                table: "Eventos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_AspNetUsers_UserId",
                table: "Eventos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Palestrantes_AspNetUsers_UserId",
                table: "Palestrantes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_AspNetUsers_UserId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Palestrantes_AspNetUsers_UserId",
                table: "Palestrantes");

            migrationBuilder.DropIndex(
                name: "IX_Palestrantes_UserId",
                table: "Palestrantes");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_UserId",
                table: "Eventos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Palestrantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Palestrantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Eventos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Eventos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Palestrantes_UserId1",
                table: "Palestrantes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_UserId1",
                table: "Eventos",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_AspNetUsers_UserId1",
                table: "Eventos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Palestrantes_AspNetUsers_UserId1",
                table: "Palestrantes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
