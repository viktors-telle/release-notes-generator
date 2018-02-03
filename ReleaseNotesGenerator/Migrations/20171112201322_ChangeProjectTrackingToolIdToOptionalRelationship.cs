using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseNotesGenerator.Migrations
{
    public partial class ChangeProjectTrackingToolIdToOptionalRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_ProjectTrackingTools_ProjectTrackingToolId",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectTrackingToolId",
                schema: "rng",
                table: "Repositories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_ProjectTrackingTools_ProjectTrackingToolId",
                schema: "rng",
                table: "Repositories",
                column: "ProjectTrackingToolId",
                principalSchema: "rng",
                principalTable: "ProjectTrackingTools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_ProjectTrackingTools_ProjectTrackingToolId",
                schema: "rng",
                table: "Repositories");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectTrackingToolId",
                schema: "rng",
                table: "Repositories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_ProjectTrackingTools_ProjectTrackingToolId",
                schema: "rng",
                table: "Repositories",
                column: "ProjectTrackingToolId",
                principalSchema: "rng",
                principalTable: "ProjectTrackingTools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
