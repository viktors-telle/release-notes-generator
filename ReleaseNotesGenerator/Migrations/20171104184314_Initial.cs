using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReleaseNotesGenerator.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rng");

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApiKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTrackingTools",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTrackingTools", x => x.Id);
                    table.UniqueConstraint("AK_ProjectTrackingTools_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectTrackingToolId = table.Column<int>(type: "int", nullable: false),
                    RepositoryType = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastCommitDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastCommitId = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RepositoryItemPaths",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    LastCommitId = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositoryItemPaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepositoryItemPaths_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "rng",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RepositoryId",
                schema: "rng",
                table: "Branches",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Name_RepositoryId",
                schema: "rng",
                table: "Branches",
                columns: new[] { "Name", "RepositoryId" },
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
                name: "IX_Repositories_Name_Url",
                schema: "rng",
                table: "Repositories",
                columns: new[] { "Name", "Url" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepositoryItemPaths_BranchId",
                schema: "rng",
                table: "RepositoryItemPaths",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RepositoryItemPaths_Path_BranchId",
                schema: "rng",
                table: "RepositoryItemPaths",
                columns: new[] { "Path", "BranchId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepositoryItemPaths",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "Repositories",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "ProjectTrackingTools",
                schema: "rng");
        }
    }
}
