using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vib17.Migrations
{
    public partial class usersessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSession_Sessions_SessionId",
                table: "UserSession");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSession_AspNetUsers_UserId",
                table: "UserSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSession",
                table: "UserSession");

            migrationBuilder.RenameTable(
                name: "UserSession",
                newName: "UserSessions");

            migrationBuilder.RenameIndex(
                name: "IX_UserSession_UserId",
                table: "UserSessions",
                newName: "IX_UserSessions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSessions",
                table: "UserSessions",
                columns: new[] { "SessionId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_Sessions_SessionId",
                table: "UserSessions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_AspNetUsers_UserId",
                table: "UserSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_Sessions_SessionId",
                table: "UserSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_AspNetUsers_UserId",
                table: "UserSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSessions",
                table: "UserSessions");

            migrationBuilder.RenameTable(
                name: "UserSessions",
                newName: "UserSession");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_UserId",
                table: "UserSession",
                newName: "IX_UserSession_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSession",
                table: "UserSession",
                columns: new[] { "SessionId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSession_Sessions_SessionId",
                table: "UserSession",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSession_AspNetUsers_UserId",
                table: "UserSession",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
