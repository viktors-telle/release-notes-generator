using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotes.Generator.Migrations
{
    public partial class AddLastCommitIdToRepositoryItemPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastCommitId",
                schema: "rng",
                table: "RepositoryItemPaths",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCommitId",
                schema: "rng",
                table: "RepositoryItemPaths");
        }
    }
}
