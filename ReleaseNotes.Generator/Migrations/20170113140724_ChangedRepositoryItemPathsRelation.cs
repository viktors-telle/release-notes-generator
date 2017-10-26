using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotes.Generator.Migrations
{
    public partial class ChangedRepositoryItemPathsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepositoryItemPaths_Repositories_RepositoryId",
                schema: "rng",
                table: "RepositoryItemPaths");

            migrationBuilder.RenameColumn(
                name: "RepositoryId",
                schema: "rng",
                table: "RepositoryItemPaths",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_RepositoryItemPaths_Path_RepositoryId",
                schema: "rng",
                table: "RepositoryItemPaths",
                newName: "IX_RepositoryItemPaths_Path_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_RepositoryItemPaths_RepositoryId",
                schema: "rng",
                table: "RepositoryItemPaths",
                newName: "IX_RepositoryItemPaths_BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepositoryItemPaths_Branches_BranchId",
                schema: "rng",
                table: "RepositoryItemPaths",
                column: "BranchId",
                principalSchema: "rng",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepositoryItemPaths_Branches_BranchId",
                schema: "rng",
                table: "RepositoryItemPaths");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                schema: "rng",
                table: "RepositoryItemPaths",
                newName: "RepositoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RepositoryItemPaths_Path_BranchId",
                schema: "rng",
                table: "RepositoryItemPaths",
                newName: "IX_RepositoryItemPaths_Path_RepositoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RepositoryItemPaths_BranchId",
                schema: "rng",
                table: "RepositoryItemPaths",
                newName: "IX_RepositoryItemPaths_RepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepositoryItemPaths_Repositories_RepositoryId",
                schema: "rng",
                table: "RepositoryItemPaths",
                column: "RepositoryId",
                principalSchema: "rng",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
