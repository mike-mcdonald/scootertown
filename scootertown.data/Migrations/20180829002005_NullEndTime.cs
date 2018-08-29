using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class NullEndTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlternateKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropColumn(
                name: "InEastPortland",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                schema: "Fact",
                table: "Deployment",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                schema: "Fact",
                table: "Deployment",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternateKey",
                schema: "Fact",
                table: "Deployment",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InEastPortland",
                schema: "Fact",
                table: "Deployment",
                nullable: false,
                defaultValue: false);
        }
    }
}
