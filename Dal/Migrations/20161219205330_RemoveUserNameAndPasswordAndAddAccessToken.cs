using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotesGenerator.Dal.Migrations
{
    public partial class RemoveUserNameAndPasswordAndAddAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "rng",
                table: "Repositories",
                newName: "AccessToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessToken",
                schema: "rng",
                table: "Repositories",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "rng",
                table: "Repositories",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
