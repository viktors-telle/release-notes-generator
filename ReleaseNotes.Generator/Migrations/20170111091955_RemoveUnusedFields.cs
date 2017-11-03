using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotes.Generator.Migrations
{
    public partial class RemoveUnusedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.DropColumn(
                name: "RepositoryTypeId",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                schema: "rng",
                table: "ProjectTrackingTools");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "rng",
                table: "ProjectTrackingTools",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                schema: "rng",
                table: "Repositories",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RepositoryTypeId",
                schema: "rng",
                table: "Repositories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "rng",
                table: "ProjectTrackingTools",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                schema: "rng",
                table: "ProjectTrackingTools",
                maxLength: 512,
                nullable: true);
        }
    }
}
