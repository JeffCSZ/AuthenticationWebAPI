using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class LoginId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_logins_loginId",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "roles");

            migrationBuilder.RenameColumn(
                name: "loginId",
                table: "roles",
                newName: "LoginId");

            migrationBuilder.RenameIndex(
                name: "IX_roles_loginId",
                table: "roles",
                newName: "IX_roles_LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_logins_LoginId",
                table: "roles",
                column: "LoginId",
                principalTable: "logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_logins_LoginId",
                table: "roles");

            migrationBuilder.RenameColumn(
                name: "LoginId",
                table: "roles",
                newName: "loginId");

            migrationBuilder.RenameIndex(
                name: "IX_roles_LoginId",
                table: "roles",
                newName: "IX_roles_loginId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_roles_logins_loginId",
                table: "roles",
                column: "loginId",
                principalTable: "logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
