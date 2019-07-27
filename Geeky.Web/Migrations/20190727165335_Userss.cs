using Microsoft.EntityFrameworkCore.Migrations;

namespace Geeky.Web.Migrations
{
    public partial class Userss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_AspNetUsers_userId",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Eventos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Eventos_userId",
                table: "Eventos",
                newName: "IX_Eventos_UserId");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_AspNetUsers_UserId",
                table: "Eventos",
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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Eventos",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Eventos_UserId",
                table: "Eventos",
                newName: "IX_Eventos_userId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Apellido");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_AspNetUsers_userId",
                table: "Eventos",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
