using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Dim");

            migrationBuilder.EnsureSchema(
                name: "Fact");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", "'postgis', '', ''");

            migrationBuilder.CreateTable(
                name: "Calendar",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Day = table.Column<byte>(nullable: false),
                    Weekday = table.Column<byte>(nullable: false),
                    WeekDayName = table.Column<string>(nullable: true),
                    IsWeekend = table.Column<bool>(nullable: false),
                    IsHoliday = table.Column<bool>(nullable: false),
                    HolidayText = table.Column<string>(nullable: true),
                    DayOfYear = table.Column<short>(nullable: false),
                    WeekOfMonth = table.Column<byte>(nullable: false),
                    WeekOfYear = table.Column<byte>(nullable: false),
                    Month = table.Column<byte>(nullable: false),
                    MonthName = table.Column<string>(nullable: true),
                    Year = table.Column<short>(nullable: false),
                    MMYYYY = table.Column<string>(nullable: true),
                    MonthYear = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PlacementReason",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementReason", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "RemovalReason",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemovalReason", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Collision",
                schema: "Fact",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CalendarKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collision", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Collision_Calendar_CalendarKey",
                        column: x => x.CalendarKey,
                        principalSchema: "Dim",
                        principalTable: "Calendar",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Complaint",
                schema: "Fact",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CalendarKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Complaint_Calendar_CalendarKey",
                        column: x => x.CalendarKey,
                        principalSchema: "Dim",
                        principalTable: "Calendar",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                schema: "Dim",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Registered = table.Column<bool>(nullable: false),
                    CompanyKey = table.Column<int>(nullable: true),
                    TypeKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Vehicle_Company_CompanyKey",
                        column: x => x.CompanyKey,
                        principalSchema: "Dim",
                        principalTable: "Company",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleType_TypeKey",
                        column: x => x.TypeKey,
                        principalSchema: "Dim",
                        principalTable: "VehicleType",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deployment",
                schema: "Fact",
                columns: table => new
                {
                    Key = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AlternateKey = table.Column<string>(maxLength: 30, nullable: true),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    FirstSeen = table.Column<DateTime>(nullable: false),
                    LastSeen = table.Column<DateTime>(nullable: false),
                    Location = table.Column<Point>(nullable: true),
                    InEastPortland = table.Column<bool>(nullable: false),
                    BatteryLevel = table.Column<byte>(nullable: false),
                    AllowedPlacement = table.Column<bool>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false),
                    Disabled = table.Column<bool>(nullable: false),
                    VehicleKey = table.Column<int>(nullable: false),
                    CompanyKey = table.Column<int>(nullable: false),
                    VehicleTypeKey = table.Column<int>(nullable: false),
                    StartDateKey = table.Column<int>(nullable: false),
                    EndDateKey = table.Column<int>(nullable: false),
                    PlacementReasonKey = table.Column<int>(nullable: false),
                    PickupReasonKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deployment", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Deployment_Company_CompanyKey",
                        column: x => x.CompanyKey,
                        principalSchema: "Dim",
                        principalTable: "Company",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deployment_Calendar_EndDateKey",
                        column: x => x.EndDateKey,
                        principalSchema: "Dim",
                        principalTable: "Calendar",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deployment_RemovalReason_PickupReasonKey",
                        column: x => x.PickupReasonKey,
                        principalSchema: "Dim",
                        principalTable: "RemovalReason",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deployment_PlacementReason_PlacementReasonKey",
                        column: x => x.PlacementReasonKey,
                        principalSchema: "Dim",
                        principalTable: "PlacementReason",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deployment_Calendar_StartDateKey",
                        column: x => x.StartDateKey,
                        principalSchema: "Dim",
                        principalTable: "Calendar",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deployment_Vehicle_VehicleKey",
                        column: x => x.VehicleKey,
                        principalSchema: "Dim",
                        principalTable: "Vehicle",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deployment_VehicleType_VehicleTypeKey",
                        column: x => x.VehicleTypeKey,
                        principalSchema: "Dim",
                        principalTable: "VehicleType",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                schema: "Fact",
                columns: table => new
                {
                    Key = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AlternateKey = table.Column<string>(maxLength: 30, nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    StartPoint = table.Column<Point>(nullable: true),
                    EndPoint = table.Column<Point>(nullable: true),
                    Route = table.Column<LineString>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    Distance = table.Column<int>(nullable: false),
                    Accuracy = table.Column<byte>(nullable: false),
                    SampleRate = table.Column<short>(nullable: false),
                    MaxSpeed = table.Column<byte>(nullable: false),
                    AverageSpeed = table.Column<byte>(nullable: false),
                    StandardCost = table.Column<int>(nullable: false),
                    ActualCost = table.Column<int>(nullable: false),
                    ParkingVerification = table.Column<string>(nullable: true),
                    VehicleKey = table.Column<int>(nullable: false),
                    CompanyKey = table.Column<int>(nullable: false),
                    VehicleTypeKey = table.Column<int>(nullable: false),
                    StartDateKey = table.Column<int>(nullable: false),
                    EndDateKey = table.Column<int>(nullable: false),
                    PaymentTypeKey = table.Column<int>(nullable: false),
                    PaymentAccessKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Trip_Company_CompanyKey",
                        column: x => x.CompanyKey,
                        principalSchema: "Dim",
                        principalTable: "Company",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_Calendar_EndDateKey",
                        column: x => x.EndDateKey,
                        principalSchema: "Dim",
                        principalTable: "Calendar",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_PaymentType_PaymentAccessKey",
                        column: x => x.PaymentAccessKey,
                        principalSchema: "Dim",
                        principalTable: "PaymentType",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_PaymentType_PaymentTypeKey",
                        column: x => x.PaymentTypeKey,
                        principalSchema: "Dim",
                        principalTable: "PaymentType",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_Calendar_StartDateKey",
                        column: x => x.StartDateKey,
                        principalSchema: "Dim",
                        principalTable: "Calendar",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_Vehicle_VehicleKey",
                        column: x => x.VehicleKey,
                        principalSchema: "Dim",
                        principalTable: "Vehicle",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_VehicleType_VehicleTypeKey",
                        column: x => x.VehicleTypeKey,
                        principalSchema: "Dim",
                        principalTable: "VehicleType",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Dim",
                table: "Company",
                columns: new[] { "Key", "Name" },
                values: new object[,]
                {
                    { 1, "Bird" },
                    { 2, "Lime" },
                    { 3, "Skip" },
                    { 4, "CycleHops" }
                });

            migrationBuilder.InsertData(
                schema: "Dim",
                table: "PaymentType",
                columns: new[] { "Key", "Name" },
                values: new object[,]
                {
                    { 1, "Phone scan" },
                    { 2, "Phone text" }
                });

            migrationBuilder.InsertData(
                schema: "Dim",
                table: "PlacementReason",
                columns: new[] { "Key", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Rebalancing" }
                });

            migrationBuilder.InsertData(
                schema: "Dim",
                table: "RemovalReason",
                columns: new[] { "Key", "Name" },
                values: new object[,]
                {
                    { 4, "Maintenance" },
                    { 3, "Out of service area" },
                    { 2, "Rebalancing" },
                    { 1, "User" }
                });

            migrationBuilder.InsertData(
                schema: "Dim",
                table: "VehicleType",
                columns: new[] { "Key", "Name" },
                values: new object[,]
                {
                    { 14, "Personal Assisted Mobility Device" },
                    { 13, "Motorcycle" },
                    { 12, "Bus" },
                    { 11, "Pedicab" },
                    { 10, "Taxi" },
                    { 9, "AV Private-for-hire vehicle" },
                    { 8, "TNC Private-for-hire vehicle" },
                    { 2, "Electric bicycle" },
                    { 6, "Motor vehicle" },
                    { 5, "None-Pedestrian" },
                    { 4, "Bicycle" },
                    { 3, "Scooter" },
                    { 15, "Private or public agency transit vehicle" },
                    { 1, "Electric scooter" },
                    { 7, "AV Motor vehicle" },
                    { 16, "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_Date",
                schema: "Dim",
                table: "Calendar",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Name",
                schema: "Dim",
                table: "Company",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentType_Name",
                schema: "Dim",
                table: "PaymentType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlacementReason_Name",
                schema: "Dim",
                table: "PlacementReason",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemovalReason_Name",
                schema: "Dim",
                table: "RemovalReason",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CompanyKey",
                schema: "Dim",
                table: "Vehicle",
                column: "CompanyKey");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Name",
                schema: "Dim",
                table: "Vehicle",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_TypeKey",
                schema: "Dim",
                table: "Vehicle",
                column: "TypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleType_Name",
                schema: "Dim",
                table: "VehicleType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collision_CalendarKey",
                schema: "Fact",
                table: "Collision",
                column: "CalendarKey");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_CalendarKey",
                schema: "Fact",
                table: "Complaint",
                column: "CalendarKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_CompanyKey",
                schema: "Fact",
                table: "Deployment",
                column: "CompanyKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_EndDateKey",
                schema: "Fact",
                table: "Deployment",
                column: "EndDateKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_PickupReasonKey",
                schema: "Fact",
                table: "Deployment",
                column: "PickupReasonKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_PlacementReasonKey",
                schema: "Fact",
                table: "Deployment",
                column: "PlacementReasonKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_StartDateKey",
                schema: "Fact",
                table: "Deployment",
                column: "StartDateKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_VehicleTypeKey",
                schema: "Fact",
                table: "Deployment",
                column: "VehicleTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_VehicleKey_StartDateKey_StartTime",
                schema: "Fact",
                table: "Deployment",
                columns: new[] { "VehicleKey", "StartDateKey", "StartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_AlternateKey",
                schema: "Fact",
                table: "Trip",
                column: "AlternateKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_CompanyKey",
                schema: "Fact",
                table: "Trip",
                column: "CompanyKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_EndDateKey",
                schema: "Fact",
                table: "Trip",
                column: "EndDateKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_PaymentAccessKey",
                schema: "Fact",
                table: "Trip",
                column: "PaymentAccessKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_PaymentTypeKey",
                schema: "Fact",
                table: "Trip",
                column: "PaymentTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_StartDateKey",
                schema: "Fact",
                table: "Trip",
                column: "StartDateKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_VehicleTypeKey",
                schema: "Fact",
                table: "Trip",
                column: "VehicleTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_VehicleKey_StartDateKey_StartTime",
                schema: "Fact",
                table: "Trip",
                columns: new[] { "VehicleKey", "StartDateKey", "StartTime" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collision",
                schema: "Fact");

            migrationBuilder.DropTable(
                name: "Complaint",
                schema: "Fact");

            migrationBuilder.DropTable(
                name: "Deployment",
                schema: "Fact");

            migrationBuilder.DropTable(
                name: "Trip",
                schema: "Fact");

            migrationBuilder.DropTable(
                name: "RemovalReason",
                schema: "Dim");

            migrationBuilder.DropTable(
                name: "PlacementReason",
                schema: "Dim");

            migrationBuilder.DropTable(
                name: "Calendar",
                schema: "Dim");

            migrationBuilder.DropTable(
                name: "PaymentType",
                schema: "Dim");

            migrationBuilder.DropTable(
                name: "Vehicle",
                schema: "Dim");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Dim");

            migrationBuilder.DropTable(
                name: "VehicleType",
                schema: "Dim");
        }
    }
}
