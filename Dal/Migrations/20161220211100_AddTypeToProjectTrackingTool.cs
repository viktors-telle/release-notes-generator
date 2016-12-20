using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotesGenerator.Dal.Migrations
{
    public partial class AddTypeToProjectTrackingTool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "rng",
                table: "ProjectTrackingTools",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "rng",
                table: "ProjectTrackingTools");
        }
    }
}
