using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManager.Migrations.UsuarioDb
{
    public partial class AtualizaTabelaUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluguel_AspNetUsers_UsuarioId",
                table: "Aluguel");

            migrationBuilder.DropIndex(
                name: "IX_Aluguel_UsuarioId",
                table: "Aluguel");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Aluguel");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Aluguel");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "AspNetUsers",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserNameId",
                table: "Aluguel",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguel_UserNameId",
                table: "Aluguel",
                column: "UserNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluguel_AspNetUsers_UserNameId",
                table: "Aluguel",
                column: "UserNameId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluguel_AspNetUsers_UserNameId",
                table: "Aluguel");

            migrationBuilder.DropIndex(
                name: "IX_Aluguel_UserNameId",
                table: "Aluguel");

            migrationBuilder.DropColumn(
                name: "UserNameId",
                table: "Aluguel");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Aluguel",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Aluguel",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguel_UsuarioId",
                table: "Aluguel",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluguel_AspNetUsers_UsuarioId",
                table: "Aluguel",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
