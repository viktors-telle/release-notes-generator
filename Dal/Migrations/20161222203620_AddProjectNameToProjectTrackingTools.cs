using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotesGenerator.Dal.Migrations
{
    public partial class AddProjectNameToProjectTrackingTools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                schema: "rng",
                table: "ProjectTrackingTools",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                schema: "rng",
                table: "ProjectTrackingTools");
        }
    }
}
