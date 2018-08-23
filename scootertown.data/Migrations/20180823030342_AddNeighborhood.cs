using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class AddNeighborhood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodEndKey",
                schema: "Fact",
                table: "Trip",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodStartKey",
                schema: "Fact",
                table: "Trip",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodKey",
                schema: "Fact",
                table: "Deployment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Neighborhood",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    AlternateKey = table.Column<string>(nullable: false),
                    Geometry = table.Column<Polygon>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhood", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_NeighborhoodEndKey",
                schema: "Fact",
                table: "Trip",
                column: "NeighborhoodEndKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_NeighborhoodStartKey",
                schema: "Fact",
                table: "Trip",
                column: "NeighborhoodStartKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_NeighborhoodKey",
                schema: "Fact",
                table: "Deployment",
                column: "NeighborhoodKey");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_AlternateKey",
                schema: "Dim",
                table: "Neighborhood",
                column: "AlternateKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_Geometry",
                schema: "Dim",
                table: "Neighborhood",
                column: "Geometry")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_Neighborhood_NeighborhoodKey",
                schema: "Fact",
                table: "Deployment",
                column: "NeighborhoodKey",
                principalSchema: "Dim",
                principalTable: "Neighborhood",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Neighborhood_NeighborhoodEndKey",
                schema: "Fact",
                table: "Trip",
                column: "NeighborhoodEndKey",
                principalSchema: "Dim",
                principalTable: "Neighborhood",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Neighborhood_NeighborhoodStartKey",
                schema: "Fact",
                table: "Trip",
                column: "NeighborhoodStartKey",
                principalSchema: "Dim",
                principalTable: "Neighborhood",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Neighborhood_NeighborhoodKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Neighborhood_NeighborhoodEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Neighborhood_NeighborhoodStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropTable(
                name: "Neighborhood",
                schema: "Dim");

            migrationBuilder.DropIndex(
                name: "IX_Trip_NeighborhoodEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_NeighborhoodStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Deployment_NeighborhoodKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropColumn(
                name: "NeighborhoodEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "NeighborhoodStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "NeighborhoodKey",
                schema: "Fact",
                table: "Deployment");
        }
    }
}
