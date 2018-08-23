using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class CreateSpatialIndicies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trip_EndPoint",
                schema: "Fact",
                table: "Trip",
                column: "EndPoint");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_Route",
                schema: "Fact",
                table: "Trip",
                column: "Route")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_StartPoint",
                schema: "Fact",
                table: "Trip",
                column: "StartPoint");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_Location",
                schema: "Fact",
                table: "Deployment",
                column: "Location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trip_EndPoint",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_Route",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_StartPoint",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Deployment_Location",
                schema: "Fact",
                table: "Deployment");
        }
    }
}
