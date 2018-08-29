using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class AddPatterArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatternAreaEndKey",
                schema: "Fact",
                table: "Trip",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatternAreaStartKey",
                schema: "Fact",
                table: "Trip",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatternAreaKey",
                schema: "Fact",
                table: "Deployment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PatternArea",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    AlternateKey = table.Column<string>(nullable: true),
                    Geometry = table.Column<Geometry>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternArea", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_PatternAreaEndKey",
                schema: "Fact",
                table: "Trip",
                column: "PatternAreaEndKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_PatternAreaStartKey",
                schema: "Fact",
                table: "Trip",
                column: "PatternAreaStartKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_PatternAreaKey",
                schema: "Fact",
                table: "Deployment",
                column: "PatternAreaKey");

            migrationBuilder.CreateIndex(
                name: "IX_PatternArea_Geometry",
                schema: "Dim",
                table: "PatternArea",
                column: "Geometry")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_PatternArea_PatternAreaKey",
                schema: "Fact",
                table: "Deployment",
                column: "PatternAreaKey",
                principalSchema: "Dim",
                principalTable: "PatternArea",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_PatternArea_PatternAreaEndKey",
                schema: "Fact",
                table: "Trip",
                column: "PatternAreaEndKey",
                principalSchema: "Dim",
                principalTable: "PatternArea",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_PatternArea_PatternAreaStartKey",
                schema: "Fact",
                table: "Trip",
                column: "PatternAreaStartKey",
                principalSchema: "Dim",
                principalTable: "PatternArea",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_PatternArea_PatternAreaKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_PatternArea_PatternAreaEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_PatternArea_PatternAreaStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropTable(
                name: "PatternArea",
                schema: "Dim");

            migrationBuilder.DropIndex(
                name: "IX_Trip_PatternAreaEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_PatternAreaStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Deployment_PatternAreaKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropColumn(
                name: "PatternAreaEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "PatternAreaStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "PatternAreaKey",
                schema: "Fact",
                table: "Deployment");
        }
    }
}
