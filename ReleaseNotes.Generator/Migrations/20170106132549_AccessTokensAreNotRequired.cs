using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotes.Generator.Migrations
{
    public partial class AccessTokensAreNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                schema: "rng",
                table: "Repositories",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                schema: "rng",
                table: "ProjectTrackingTools",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                schema: "rng",
                table: "Repositories",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                schema: "rng",
                table: "ProjectTrackingTools",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);
        }
    }
}
