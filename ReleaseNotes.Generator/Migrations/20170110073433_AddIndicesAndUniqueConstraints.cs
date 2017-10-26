using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotes.Generator.Migrations
{
    public partial class AddIndicesAndUniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProjectTrackingTools_Name",
                schema: "rng",
                table: "ProjectTrackingTools",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_Name",
                schema: "rng",
                table: "Repositories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ApiKey",
                schema: "rng",
                table: "Projects",
                column: "ApiKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IsDeactivated",
                schema: "rng",
                table: "Projects",
                column: "IsDeactivated");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                schema: "rng",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Name",
                schema: "rng",
                table: "Branches",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Repositories_Name",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProjectTrackingTools_Name",
                schema: "rng",
                table: "ProjectTrackingTools");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ApiKey",
                schema: "rng",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_IsDeactivated",
                schema: "rng",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Name",
                schema: "rng",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Branches_Name",
                schema: "rng",
                table: "Branches");
        }
    }
}
