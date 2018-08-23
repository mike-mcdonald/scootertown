using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class DeploymentFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deployment_VehicleKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_VehicleKey_StartDateKey_StartTime",
                schema: "Fact",
                table: "Deployment",
                columns: new[] { "VehicleKey", "StartDateKey", "StartTime" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deployment_VehicleKey_StartDateKey_StartTime",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_VehicleKey",
                schema: "Fact",
                table: "Deployment",
                column: "VehicleKey");
        }
    }
}
