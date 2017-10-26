using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReleaseNotes.Generator.Migrations
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApiKey = table.Column<string>(maxLength: 128, nullable: false),
                    IsDeactivated = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessToken = table.Column<string>(maxLength: 512, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    ProjectName = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 4096, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTrackingTools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                schema: "rng",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessToken = table.Column<string>(maxLength: 512, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectTrackingToolId = table.Column<int>(nullable: false),
                    RepositoryType = table.Column<int>(nullable: false),
                    RepositoryTypeId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 4096, nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastCommitDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "Projects",
                schema: "rng");

            migrationBuilder.DropTable(
                name: "ProjectTrackingTools",
                schema: "rng");
        }
    }
}
