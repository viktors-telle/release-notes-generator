using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReleaseNotes.Generator.Migrations
{
    public partial class AddRepositoryItemPaths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepositoryItemPaths",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 256, nullable: false),
                    RepositoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositoryItemPaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepositoryItemPaths_Repositories_RepositoryId",
                        column: x => x.RepositoryId,
                        principalSchema: "rng",
                        principalTable: "Repositories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepositoryItemPaths_RepositoryId",
                schema: "rng",
                table: "RepositoryItemPaths",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RepositoryItemPaths_Path_RepositoryId",
                schema: "rng",
                table: "RepositoryItemPaths",
                columns: new[] { "Path", "RepositoryId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepositoryItemPaths",
                schema: "rng");
        }
    }
}
