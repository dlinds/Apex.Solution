using Microsoft.EntityFrameworkCore.Migrations;

namespace Apex.Solution.Migrations
{
    public partial class RemoveAdministratorFromUserQuery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQueries_AspNetUsers_AdministratorId",
                table: "UserQueries");

            migrationBuilder.DropIndex(
                name: "IX_UserQueries_AdministratorId",
                table: "UserQueries");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "UserQueries");

            migrationBuilder.AlterColumn<int>(
                name: "CallCount",
                table: "Commands",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdministratorId",
                table: "UserQueries",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CallCount",
                table: "Commands",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserQueries_AdministratorId",
                table: "UserQueries",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQueries_AspNetUsers_AdministratorId",
                table: "UserQueries",
                column: "AdministratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
