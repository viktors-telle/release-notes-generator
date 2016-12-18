using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReleaseNotesGenerator.Dal.Migrations
{
    public partial class InitialDbStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTrackingTools",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Password = table.Column<string>(maxLength: 512, nullable: false),
                    Url = table.Column<string>(maxLength: 4096, nullable: false),
                    UserName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTrackingTools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepositoryTypes",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Password = table.Column<string>(maxLength: 512, nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectTrackingToolId = table.Column<int>(nullable: false),
                    RepositoryTypeId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 4096, nullable: false),
                    UserName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repositories_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "rng",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repositories_ProjectTrackingTools_ProjectTrackingToolId",
                        column: x => x.ProjectTrackingToolId,
                        principalSchema: "rng",
                        principalTable: "ProjectTrackingTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repositories_RepositoryTypes_RepositoryTypeId",
                        column: x => x.RepositoryTypeId,
                        principalSchema: "rng",
                        principalTable: "RepositoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastCommitId = table.Column<string>(maxLength: 512, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    RepositoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Repositories_RepositoryId",
                        column: x => x.RepositoryId,
                        principalSchema: "rng",
                        principalTable: "Repositories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RepositoryId",
                schema: "rng",
                table: "Branches",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_ProjectId",
                schema: "rng",
                table: "Repositories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_ProjectTrackingToolId",
                schema: "rng",
                table: "Repositories",
                column: "ProjectTrackingToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_RepositoryTypeId",
                schema: "rng",
                table: "Repositories",
                column: "RepositoryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "Repositories",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "ProjectTrackingTools",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "RepositoryTypes",
                schema: "rng");
        }
    }
}
