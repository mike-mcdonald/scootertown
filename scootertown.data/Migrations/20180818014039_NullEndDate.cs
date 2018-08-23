using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class NullEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Calendar_EndDateKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.AlterColumn<int>(
                name: "EndDateKey",
                schema: "Fact",
                table: "Deployment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_Calendar_EndDateKey",
                schema: "Fact",
                table: "Deployment",
                column: "EndDateKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Calendar_EndDateKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.AlterColumn<int>(
                name: "EndDateKey",
                schema: "Fact",
                table: "Deployment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_Calendar_EndDateKey",
                schema: "Fact",
                table: "Deployment",
                column: "EndDateKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
