using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotes.Generator.Migrations
{
    public partial class AddCompositeUniqueIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Repositories_Name",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.DropIndex(
                name: "IX_Branches_Name",
                schema: "rng",
                table: "Branches");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_Name_Url",
                schema: "rng",
                table: "Repositories",
                columns: new[] { "Name", "Url" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Name_RepositoryId",
                schema: "rng",
                table: "Branches",
                columns: new[] { "Name", "RepositoryId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Repositories_Name_Url",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.DropIndex(
                name: "IX_Branches_Name_RepositoryId",
                schema: "rng",
                table: "Branches");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_Name",
                schema: "rng",
                table: "Repositories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Name",
                schema: "rng",
                table: "Branches",
                column: "Name",
                unique: true);
        }
    }
}
