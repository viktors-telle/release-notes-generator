using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotesGenerator.Dal.Migrations
{
    public partial class RemoveUserNameAndPasswordFromTrackingToolsAndAddAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "rng",
                table: "ProjectTrackingTools");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "rng",
                table: "ProjectTrackingTools",
                newName: "AccessToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessToken",
                schema: "rng",
                table: "ProjectTrackingTools",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "rng",
                table: "ProjectTrackingTools",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
