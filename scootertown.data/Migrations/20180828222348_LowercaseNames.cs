using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.PBOT.Scootertown.Data.Migrations
{
    public partial class LowercaseNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Company_CompanyKey",
                schema: "Dim",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleType_TypeKey",
                schema: "Dim",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Collision_Calendar_CalendarKey",
                schema: "Fact",
                table: "Collision");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Calendar_CalendarKey",
                schema: "Fact",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Company_CompanyKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Calendar_EndDateKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Neighborhood_NeighborhoodKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_PatternArea_PatternAreaKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_RemovalReason_PickupReasonKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_PlacementReason_PlacementReasonKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Calendar_StartDateKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_Vehicle_VehicleKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Deployment_VehicleType_VehicleTypeKey",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Company_CompanyKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Calendar_EndDateKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Neighborhood_NeighborhoodEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Neighborhood_NeighborhoodStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_PatternArea_PatternAreaEndKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_PatternArea_PatternAreaStartKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_PaymentType_PaymentAccessKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_PaymentType_PaymentTypeKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Calendar_StartDateKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Vehicle_VehicleKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_VehicleType_VehicleTypeKey",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                schema: "Fact",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deployment",
                schema: "Fact",
                table: "Deployment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaint",
                schema: "Fact",
                table: "Complaint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collision",
                schema: "Fact",
                table: "Collision");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleType",
                schema: "Dim",
                table: "VehicleType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                schema: "Dim",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RemovalReason",
                schema: "Dim",
                table: "RemovalReason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlacementReason",
                schema: "Dim",
                table: "PlacementReason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentType",
                schema: "Dim",
                table: "PaymentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatternArea",
                schema: "Dim",
                table: "PatternArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Neighborhood",
                schema: "Dim",
                table: "Neighborhood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                schema: "Dim",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calendar",
                schema: "Dim",
                table: "Calendar");

            migrationBuilder.EnsureSchema(
                name: "dim");

            migrationBuilder.EnsureSchema(
                name: "fact");

            migrationBuilder.RenameTable(
                name: "Trip",
                schema: "Fact",
                newName: "trip",
                newSchema: "fact");

            migrationBuilder.RenameTable(
                name: "Deployment",
                schema: "Fact",
                newName: "deployment",
                newSchema: "fact");

            migrationBuilder.RenameTable(
                name: "Complaint",
                schema: "Fact",
                newName: "complaint",
                newSchema: "fact");

            migrationBuilder.RenameTable(
                name: "Collision",
                schema: "Fact",
                newName: "collision",
                newSchema: "fact");

            migrationBuilder.RenameTable(
                name: "VehicleType",
                schema: "Dim",
                newName: "vehicletype",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                schema: "Dim",
                newName: "vehicle",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "RemovalReason",
                schema: "Dim",
                newName: "removalreason",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "PlacementReason",
                schema: "Dim",
                newName: "placementreason",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "PaymentType",
                schema: "Dim",
                newName: "paymenttype",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "PatternArea",
                schema: "Dim",
                newName: "patternarea",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "Neighborhood",
                schema: "Dim",
                newName: "neighborhood",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "Company",
                schema: "Dim",
                newName: "company",
                newSchema: "dim");

            migrationBuilder.RenameTable(
                name: "Calendar",
                schema: "Dim",
                newName: "calendar",
                newSchema: "dim");

            migrationBuilder.RenameColumn(
                name: "VehicleTypeKey",
                schema: "fact",
                table: "trip",
                newName: "vehicletypekey");

            migrationBuilder.RenameColumn(
                name: "VehicleKey",
                schema: "fact",
                table: "trip",
                newName: "vehiclekey");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                schema: "fact",
                table: "trip",
                newName: "starttime");

            migrationBuilder.RenameColumn(
                name: "StartPoint",
                schema: "fact",
                table: "trip",
                newName: "startpoint");

            migrationBuilder.RenameColumn(
                name: "StartDateKey",
                schema: "fact",
                table: "trip",
                newName: "startdatekey");

            migrationBuilder.RenameColumn(
                name: "StandardCost",
                schema: "fact",
                table: "trip",
                newName: "standardcost");

            migrationBuilder.RenameColumn(
                name: "SampleRate",
                schema: "fact",
                table: "trip",
                newName: "samplerate");

            migrationBuilder.RenameColumn(
                name: "Route",
                schema: "fact",
                table: "trip",
                newName: "route");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeKey",
                schema: "fact",
                table: "trip",
                newName: "paymenttypekey");

            migrationBuilder.RenameColumn(
                name: "PaymentAccessKey",
                schema: "fact",
                table: "trip",
                newName: "paymentaccesskey");

            migrationBuilder.RenameColumn(
                name: "PatternAreaStartKey",
                schema: "fact",
                table: "trip",
                newName: "patternareastartkey");

            migrationBuilder.RenameColumn(
                name: "PatternAreaEndKey",
                schema: "fact",
                table: "trip",
                newName: "patternareaendkey");

            migrationBuilder.RenameColumn(
                name: "ParkingVerification",
                schema: "fact",
                table: "trip",
                newName: "parkingverification");

            migrationBuilder.RenameColumn(
                name: "NeighborhoodStartKey",
                schema: "fact",
                table: "trip",
                newName: "neighborhoodstartkey");

            migrationBuilder.RenameColumn(
                name: "NeighborhoodEndKey",
                schema: "fact",
                table: "trip",
                newName: "neighborhoodendkey");

            migrationBuilder.RenameColumn(
                name: "MaxSpeed",
                schema: "fact",
                table: "trip",
                newName: "maxspeed");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                schema: "fact",
                table: "trip",
                newName: "endtime");

            migrationBuilder.RenameColumn(
                name: "EndPoint",
                schema: "fact",
                table: "trip",
                newName: "endpoint");

            migrationBuilder.RenameColumn(
                name: "EndDateKey",
                schema: "fact",
                table: "trip",
                newName: "enddatekey");

            migrationBuilder.RenameColumn(
                name: "Duration",
                schema: "fact",
                table: "trip",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "Distance",
                schema: "fact",
                table: "trip",
                newName: "distance");

            migrationBuilder.RenameColumn(
                name: "CompanyKey",
                schema: "fact",
                table: "trip",
                newName: "companykey");

            migrationBuilder.RenameColumn(
                name: "AverageSpeed",
                schema: "fact",
                table: "trip",
                newName: "averagespeed");

            migrationBuilder.RenameColumn(
                name: "AlternateKey",
                schema: "fact",
                table: "trip",
                newName: "alternatekey");

            migrationBuilder.RenameColumn(
                name: "ActualCost",
                schema: "fact",
                table: "trip",
                newName: "actualcost");

            migrationBuilder.RenameColumn(
                name: "Accuracy",
                schema: "fact",
                table: "trip",
                newName: "accuracy");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "fact",
                table: "trip",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_VehicleKey_StartDateKey_StartTime",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_vehiclekey_startdatekey_starttime");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_VehicleTypeKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_vehicletypekey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_StartPoint",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_startpoint");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_StartDateKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_startdatekey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_Route",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_route");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_PaymentTypeKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_paymenttypekey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_PaymentAccessKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_paymentaccesskey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_PatternAreaStartKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_patternareastartkey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_PatternAreaEndKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_patternareaendkey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_NeighborhoodStartKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_neighborhoodstartkey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_NeighborhoodEndKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_neighborhoodendkey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_EndPoint",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_endpoint");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_EndDateKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_enddatekey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_CompanyKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_companykey");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_AlternateKey",
                schema: "fact",
                table: "trip",
                newName: "IX_trip_alternatekey");

            migrationBuilder.RenameColumn(
                name: "VehicleTypeKey",
                schema: "fact",
                table: "deployment",
                newName: "vehicletypekey");

            migrationBuilder.RenameColumn(
                name: "VehicleKey",
                schema: "fact",
                table: "deployment",
                newName: "vehiclekey");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                schema: "fact",
                table: "deployment",
                newName: "starttime");

            migrationBuilder.RenameColumn(
                name: "StartDateKey",
                schema: "fact",
                table: "deployment",
                newName: "startdatekey");

            migrationBuilder.RenameColumn(
                name: "Reserved",
                schema: "fact",
                table: "deployment",
                newName: "reserved");

            migrationBuilder.RenameColumn(
                name: "PlacementReasonKey",
                schema: "fact",
                table: "deployment",
                newName: "placementreasonkey");

            migrationBuilder.RenameColumn(
                name: "PickupReasonKey",
                schema: "fact",
                table: "deployment",
                newName: "pickupreasonkey");

            migrationBuilder.RenameColumn(
                name: "PatternAreaKey",
                schema: "fact",
                table: "deployment",
                newName: "patternareakey");

            migrationBuilder.RenameColumn(
                name: "NeighborhoodKey",
                schema: "fact",
                table: "deployment",
                newName: "neighborhoodkey");

            migrationBuilder.RenameColumn(
                name: "Location",
                schema: "fact",
                table: "deployment",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "LastSeen",
                schema: "fact",
                table: "deployment",
                newName: "lastseen");

            migrationBuilder.RenameColumn(
                name: "InEastPortland",
                schema: "fact",
                table: "deployment",
                newName: "ineastportland");

            migrationBuilder.RenameColumn(
                name: "FirstSeen",
                schema: "fact",
                table: "deployment",
                newName: "firstseen");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                schema: "fact",
                table: "deployment",
                newName: "endtime");

            migrationBuilder.RenameColumn(
                name: "EndDateKey",
                schema: "fact",
                table: "deployment",
                newName: "enddatekey");

            migrationBuilder.RenameColumn(
                name: "Disabled",
                schema: "fact",
                table: "deployment",
                newName: "disabled");

            migrationBuilder.RenameColumn(
                name: "CompanyKey",
                schema: "fact",
                table: "deployment",
                newName: "companykey");

            migrationBuilder.RenameColumn(
                name: "BatteryLevel",
                schema: "fact",
                table: "deployment",
                newName: "batterylevel");

            migrationBuilder.RenameColumn(
                name: "AlternateKey",
                schema: "fact",
                table: "deployment",
                newName: "alternatekey");

            migrationBuilder.RenameColumn(
                name: "AllowedPlacement",
                schema: "fact",
                table: "deployment",
                newName: "allowedplacement");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "fact",
                table: "deployment",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_VehicleKey_StartDateKey_StartTime",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_vehiclekey_startdatekey_starttime");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_VehicleTypeKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_vehicletypekey");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_StartDateKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_startdatekey");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_PlacementReasonKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_placementreasonkey");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_PickupReasonKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_pickupreasonkey");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_PatternAreaKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_patternareakey");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_NeighborhoodKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_neighborhoodkey");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_Location",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_location");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_EndDateKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_enddatekey");

            migrationBuilder.RenameIndex(
                name: "IX_Deployment_CompanyKey",
                schema: "fact",
                table: "deployment",
                newName: "IX_deployment_companykey");

            migrationBuilder.RenameColumn(
                name: "CalendarKey",
                schema: "fact",
                table: "complaint",
                newName: "calendarkey");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "fact",
                table: "complaint",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_CalendarKey",
                schema: "fact",
                table: "complaint",
                newName: "IX_complaint_calendarkey");

            migrationBuilder.RenameColumn(
                name: "CalendarKey",
                schema: "fact",
                table: "collision",
                newName: "calendarkey");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "fact",
                table: "collision",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Collision_CalendarKey",
                schema: "fact",
                table: "collision",
                newName: "IX_collision_calendarkey");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "vehicletype",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "vehicletype",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleType_Name",
                schema: "dim",
                table: "vehicletype",
                newName: "IX_vehicletype_name");

            migrationBuilder.RenameColumn(
                name: "TypeKey",
                schema: "dim",
                table: "vehicle",
                newName: "typekey");

            migrationBuilder.RenameColumn(
                name: "Registered",
                schema: "dim",
                table: "vehicle",
                newName: "registered");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "vehicle",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CompanyKey",
                schema: "dim",
                table: "vehicle",
                newName: "companykey");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "vehicle",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_TypeKey",
                schema: "dim",
                table: "vehicle",
                newName: "IX_vehicle_typekey");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_Name",
                schema: "dim",
                table: "vehicle",
                newName: "IX_vehicle_name");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_CompanyKey",
                schema: "dim",
                table: "vehicle",
                newName: "IX_vehicle_companykey");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "removalreason",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "removalreason",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_RemovalReason_Name",
                schema: "dim",
                table: "removalreason",
                newName: "IX_removalreason_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "placementreason",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "placementreason",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_PlacementReason_Name",
                schema: "dim",
                table: "placementreason",
                newName: "IX_placementreason_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "paymenttype",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "paymenttype",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentType_Name",
                schema: "dim",
                table: "paymenttype",
                newName: "IX_paymenttype_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "patternarea",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Geometry",
                schema: "dim",
                table: "patternarea",
                newName: "geometry");

            migrationBuilder.RenameColumn(
                name: "AlternateKey",
                schema: "dim",
                table: "patternarea",
                newName: "alternatekey");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "patternarea",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_PatternArea_Geometry",
                schema: "dim",
                table: "patternarea",
                newName: "IX_patternarea_geometry");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "neighborhood",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Geometry",
                schema: "dim",
                table: "neighborhood",
                newName: "geometry");

            migrationBuilder.RenameColumn(
                name: "AlternateKey",
                schema: "dim",
                table: "neighborhood",
                newName: "alternatekey");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "neighborhood",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Neighborhood_Geometry",
                schema: "dim",
                table: "neighborhood",
                newName: "IX_neighborhood_geometry");

            migrationBuilder.RenameIndex(
                name: "IX_Neighborhood_AlternateKey",
                schema: "dim",
                table: "neighborhood",
                newName: "IX_neighborhood_alternatekey");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dim",
                table: "company",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "company",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Company_Name",
                schema: "dim",
                table: "company",
                newName: "IX_company_name");

            migrationBuilder.RenameColumn(
                name: "Year",
                schema: "dim",
                table: "calendar",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Weekday",
                schema: "dim",
                table: "calendar",
                newName: "weekday");

            migrationBuilder.RenameColumn(
                name: "WeekOfYear",
                schema: "dim",
                table: "calendar",
                newName: "weekofyear");

            migrationBuilder.RenameColumn(
                name: "WeekOfMonth",
                schema: "dim",
                table: "calendar",
                newName: "weekofmonth");

            migrationBuilder.RenameColumn(
                name: "WeekDayName",
                schema: "dim",
                table: "calendar",
                newName: "weekdayname");

            migrationBuilder.RenameColumn(
                name: "MonthYear",
                schema: "dim",
                table: "calendar",
                newName: "monthyear");

            migrationBuilder.RenameColumn(
                name: "MonthName",
                schema: "dim",
                table: "calendar",
                newName: "monthname");

            migrationBuilder.RenameColumn(
                name: "Month",
                schema: "dim",
                table: "calendar",
                newName: "month");

            migrationBuilder.RenameColumn(
                name: "MMYYYY",
                schema: "dim",
                table: "calendar",
                newName: "mmyyyy");

            migrationBuilder.RenameColumn(
                name: "IsWeekend",
                schema: "dim",
                table: "calendar",
                newName: "isweekend");

            migrationBuilder.RenameColumn(
                name: "IsHoliday",
                schema: "dim",
                table: "calendar",
                newName: "isholiday");

            migrationBuilder.RenameColumn(
                name: "HolidayText",
                schema: "dim",
                table: "calendar",
                newName: "holidaytext");

            migrationBuilder.RenameColumn(
                name: "DayOfYear",
                schema: "dim",
                table: "calendar",
                newName: "dayofyear");

            migrationBuilder.RenameColumn(
                name: "Day",
                schema: "dim",
                table: "calendar",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "dim",
                table: "calendar",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "dim",
                table: "calendar",
                newName: "key");

            migrationBuilder.RenameIndex(
                name: "IX_Calendar_Date",
                schema: "dim",
                table: "calendar",
                newName: "IX_calendar_date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trip",
                schema: "fact",
                table: "trip",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deployment",
                schema: "fact",
                table: "deployment",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_complaint",
                schema: "fact",
                table: "complaint",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_collision",
                schema: "fact",
                table: "collision",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicletype",
                schema: "dim",
                table: "vehicletype",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicle",
                schema: "dim",
                table: "vehicle",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_removalreason",
                schema: "dim",
                table: "removalreason",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_placementreason",
                schema: "dim",
                table: "placementreason",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paymenttype",
                schema: "dim",
                table: "paymenttype",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_patternarea",
                schema: "dim",
                table: "patternarea",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_neighborhood",
                schema: "dim",
                table: "neighborhood",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_company",
                schema: "dim",
                table: "company",
                column: "key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_calendar",
                schema: "dim",
                table: "calendar",
                column: "key");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_company_companykey",
                schema: "dim",
                table: "vehicle",
                column: "companykey",
                principalSchema: "dim",
                principalTable: "company",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_vehicletype_typekey",
                schema: "dim",
                table: "vehicle",
                column: "typekey",
                principalSchema: "dim",
                principalTable: "vehicletype",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_collision_calendar_calendarkey",
                schema: "fact",
                table: "collision",
                column: "calendarkey",
                principalSchema: "dim",
                principalTable: "calendar",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_complaint_calendar_calendarkey",
                schema: "fact",
                table: "complaint",
                column: "calendarkey",
                principalSchema: "dim",
                principalTable: "calendar",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_company_companykey",
                schema: "fact",
                table: "deployment",
                column: "companykey",
                principalSchema: "dim",
                principalTable: "company",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_calendar_enddatekey",
                schema: "fact",
                table: "deployment",
                column: "enddatekey",
                principalSchema: "dim",
                principalTable: "calendar",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_neighborhood_neighborhoodkey",
                schema: "fact",
                table: "deployment",
                column: "neighborhoodkey",
                principalSchema: "dim",
                principalTable: "neighborhood",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_patternarea_patternareakey",
                schema: "fact",
                table: "deployment",
                column: "patternareakey",
                principalSchema: "dim",
                principalTable: "patternarea",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_removalreason_pickupreasonkey",
                schema: "fact",
                table: "deployment",
                column: "pickupreasonkey",
                principalSchema: "dim",
                principalTable: "removalreason",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_placementreason_placementreasonkey",
                schema: "fact",
                table: "deployment",
                column: "placementreasonkey",
                principalSchema: "dim",
                principalTable: "placementreason",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_calendar_startdatekey",
                schema: "fact",
                table: "deployment",
                column: "startdatekey",
                principalSchema: "dim",
                principalTable: "calendar",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_vehicle_vehiclekey",
                schema: "fact",
                table: "deployment",
                column: "vehiclekey",
                principalSchema: "dim",
                principalTable: "vehicle",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deployment_vehicletype_vehicletypekey",
                schema: "fact",
                table: "deployment",
                column: "vehicletypekey",
                principalSchema: "dim",
                principalTable: "vehicletype",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_company_companykey",
                schema: "fact",
                table: "trip",
                column: "companykey",
                principalSchema: "dim",
                principalTable: "company",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_calendar_enddatekey",
                schema: "fact",
                table: "trip",
                column: "enddatekey",
                principalSchema: "dim",
                principalTable: "calendar",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_neighborhood_neighborhoodendkey",
                schema: "fact",
                table: "trip",
                column: "neighborhoodendkey",
                principalSchema: "dim",
                principalTable: "neighborhood",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_neighborhood_neighborhoodstartkey",
                schema: "fact",
                table: "trip",
                column: "neighborhoodstartkey",
                principalSchema: "dim",
                principalTable: "neighborhood",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_patternarea_patternareaendkey",
                schema: "fact",
                table: "trip",
                column: "patternareaendkey",
                principalSchema: "dim",
                principalTable: "patternarea",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_patternarea_patternareastartkey",
                schema: "fact",
                table: "trip",
                column: "patternareastartkey",
                principalSchema: "dim",
                principalTable: "patternarea",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_paymenttype_paymentaccesskey",
                schema: "fact",
                table: "trip",
                column: "paymentaccesskey",
                principalSchema: "dim",
                principalTable: "paymenttype",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_paymenttype_paymenttypekey",
                schema: "fact",
                table: "trip",
                column: "paymenttypekey",
                principalSchema: "dim",
                principalTable: "paymenttype",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_calendar_startdatekey",
                schema: "fact",
                table: "trip",
                column: "startdatekey",
                principalSchema: "dim",
                principalTable: "calendar",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_vehicle_vehiclekey",
                schema: "fact",
                table: "trip",
                column: "vehiclekey",
                principalSchema: "dim",
                principalTable: "vehicle",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_vehicletype_vehicletypekey",
                schema: "fact",
                table: "trip",
                column: "vehicletypekey",
                principalSchema: "dim",
                principalTable: "vehicletype",
                principalColumn: "key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_company_companykey",
                schema: "dim",
                table: "vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_vehicletype_typekey",
                schema: "dim",
                table: "vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_collision_calendar_calendarkey",
                schema: "fact",
                table: "collision");

            migrationBuilder.DropForeignKey(
                name: "FK_complaint_calendar_calendarkey",
                schema: "fact",
                table: "complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_company_companykey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_calendar_enddatekey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_neighborhood_neighborhoodkey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_patternarea_patternareakey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_removalreason_pickupreasonkey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_placementreason_placementreasonkey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_calendar_startdatekey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_vehicle_vehiclekey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_deployment_vehicletype_vehicletypekey",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_company_companykey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_calendar_enddatekey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_neighborhood_neighborhoodendkey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_neighborhood_neighborhoodstartkey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_patternarea_patternareaendkey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_patternarea_patternareastartkey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_paymenttype_paymentaccesskey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_paymenttype_paymenttypekey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_calendar_startdatekey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_vehicle_vehiclekey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_vehicletype_vehicletypekey",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trip",
                schema: "fact",
                table: "trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deployment",
                schema: "fact",
                table: "deployment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_complaint",
                schema: "fact",
                table: "complaint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_collision",
                schema: "fact",
                table: "collision");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicletype",
                schema: "dim",
                table: "vehicletype");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicle",
                schema: "dim",
                table: "vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_removalreason",
                schema: "dim",
                table: "removalreason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_placementreason",
                schema: "dim",
                table: "placementreason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paymenttype",
                schema: "dim",
                table: "paymenttype");

            migrationBuilder.DropPrimaryKey(
                name: "PK_patternarea",
                schema: "dim",
                table: "patternarea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_neighborhood",
                schema: "dim",
                table: "neighborhood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company",
                schema: "dim",
                table: "company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_calendar",
                schema: "dim",
                table: "calendar");

            migrationBuilder.EnsureSchema(
                name: "Dim");

            migrationBuilder.EnsureSchema(
                name: "Fact");

            migrationBuilder.RenameTable(
                name: "trip",
                schema: "fact",
                newName: "Trip",
                newSchema: "Fact");

            migrationBuilder.RenameTable(
                name: "deployment",
                schema: "fact",
                newName: "Deployment",
                newSchema: "Fact");

            migrationBuilder.RenameTable(
                name: "complaint",
                schema: "fact",
                newName: "Complaint",
                newSchema: "Fact");

            migrationBuilder.RenameTable(
                name: "collision",
                schema: "fact",
                newName: "Collision",
                newSchema: "Fact");

            migrationBuilder.RenameTable(
                name: "vehicletype",
                schema: "dim",
                newName: "VehicleType",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "vehicle",
                schema: "dim",
                newName: "Vehicle",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "removalreason",
                schema: "dim",
                newName: "RemovalReason",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "placementreason",
                schema: "dim",
                newName: "PlacementReason",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "paymenttype",
                schema: "dim",
                newName: "PaymentType",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "patternarea",
                schema: "dim",
                newName: "PatternArea",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "neighborhood",
                schema: "dim",
                newName: "Neighborhood",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "company",
                schema: "dim",
                newName: "Company",
                newSchema: "Dim");

            migrationBuilder.RenameTable(
                name: "calendar",
                schema: "dim",
                newName: "Calendar",
                newSchema: "Dim");

            migrationBuilder.RenameColumn(
                name: "vehicletypekey",
                schema: "Fact",
                table: "Trip",
                newName: "VehicleTypeKey");

            migrationBuilder.RenameColumn(
                name: "vehiclekey",
                schema: "Fact",
                table: "Trip",
                newName: "VehicleKey");

            migrationBuilder.RenameColumn(
                name: "starttime",
                schema: "Fact",
                table: "Trip",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "startpoint",
                schema: "Fact",
                table: "Trip",
                newName: "StartPoint");

            migrationBuilder.RenameColumn(
                name: "startdatekey",
                schema: "Fact",
                table: "Trip",
                newName: "StartDateKey");

            migrationBuilder.RenameColumn(
                name: "standardcost",
                schema: "Fact",
                table: "Trip",
                newName: "StandardCost");

            migrationBuilder.RenameColumn(
                name: "samplerate",
                schema: "Fact",
                table: "Trip",
                newName: "SampleRate");

            migrationBuilder.RenameColumn(
                name: "route",
                schema: "Fact",
                table: "Trip",
                newName: "Route");

            migrationBuilder.RenameColumn(
                name: "paymenttypekey",
                schema: "Fact",
                table: "Trip",
                newName: "PaymentTypeKey");

            migrationBuilder.RenameColumn(
                name: "paymentaccesskey",
                schema: "Fact",
                table: "Trip",
                newName: "PaymentAccessKey");

            migrationBuilder.RenameColumn(
                name: "patternareastartkey",
                schema: "Fact",
                table: "Trip",
                newName: "PatternAreaStartKey");

            migrationBuilder.RenameColumn(
                name: "patternareaendkey",
                schema: "Fact",
                table: "Trip",
                newName: "PatternAreaEndKey");

            migrationBuilder.RenameColumn(
                name: "parkingverification",
                schema: "Fact",
                table: "Trip",
                newName: "ParkingVerification");

            migrationBuilder.RenameColumn(
                name: "neighborhoodstartkey",
                schema: "Fact",
                table: "Trip",
                newName: "NeighborhoodStartKey");

            migrationBuilder.RenameColumn(
                name: "neighborhoodendkey",
                schema: "Fact",
                table: "Trip",
                newName: "NeighborhoodEndKey");

            migrationBuilder.RenameColumn(
                name: "maxspeed",
                schema: "Fact",
                table: "Trip",
                newName: "MaxSpeed");

            migrationBuilder.RenameColumn(
                name: "endtime",
                schema: "Fact",
                table: "Trip",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "endpoint",
                schema: "Fact",
                table: "Trip",
                newName: "EndPoint");

            migrationBuilder.RenameColumn(
                name: "enddatekey",
                schema: "Fact",
                table: "Trip",
                newName: "EndDateKey");

            migrationBuilder.RenameColumn(
                name: "duration",
                schema: "Fact",
                table: "Trip",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "distance",
                schema: "Fact",
                table: "Trip",
                newName: "Distance");

            migrationBuilder.RenameColumn(
                name: "companykey",
                schema: "Fact",
                table: "Trip",
                newName: "CompanyKey");

            migrationBuilder.RenameColumn(
                name: "averagespeed",
                schema: "Fact",
                table: "Trip",
                newName: "AverageSpeed");

            migrationBuilder.RenameColumn(
                name: "alternatekey",
                schema: "Fact",
                table: "Trip",
                newName: "AlternateKey");

            migrationBuilder.RenameColumn(
                name: "actualcost",
                schema: "Fact",
                table: "Trip",
                newName: "ActualCost");

            migrationBuilder.RenameColumn(
                name: "accuracy",
                schema: "Fact",
                table: "Trip",
                newName: "Accuracy");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Fact",
                table: "Trip",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_trip_vehiclekey_startdatekey_starttime",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_VehicleKey_StartDateKey_StartTime");

            migrationBuilder.RenameIndex(
                name: "IX_trip_vehicletypekey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_VehicleTypeKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_startpoint",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_StartPoint");

            migrationBuilder.RenameIndex(
                name: "IX_trip_startdatekey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_StartDateKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_route",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_Route");

            migrationBuilder.RenameIndex(
                name: "IX_trip_paymenttypekey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_PaymentTypeKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_paymentaccesskey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_PaymentAccessKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_patternareastartkey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_PatternAreaStartKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_patternareaendkey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_PatternAreaEndKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_neighborhoodstartkey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_NeighborhoodStartKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_neighborhoodendkey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_NeighborhoodEndKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_endpoint",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_EndPoint");

            migrationBuilder.RenameIndex(
                name: "IX_trip_enddatekey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_EndDateKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_companykey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_CompanyKey");

            migrationBuilder.RenameIndex(
                name: "IX_trip_alternatekey",
                schema: "Fact",
                table: "Trip",
                newName: "IX_Trip_AlternateKey");

            migrationBuilder.RenameColumn(
                name: "vehicletypekey",
                schema: "Fact",
                table: "Deployment",
                newName: "VehicleTypeKey");

            migrationBuilder.RenameColumn(
                name: "vehiclekey",
                schema: "Fact",
                table: "Deployment",
                newName: "VehicleKey");

            migrationBuilder.RenameColumn(
                name: "starttime",
                schema: "Fact",
                table: "Deployment",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "startdatekey",
                schema: "Fact",
                table: "Deployment",
                newName: "StartDateKey");

            migrationBuilder.RenameColumn(
                name: "reserved",
                schema: "Fact",
                table: "Deployment",
                newName: "Reserved");

            migrationBuilder.RenameColumn(
                name: "placementreasonkey",
                schema: "Fact",
                table: "Deployment",
                newName: "PlacementReasonKey");

            migrationBuilder.RenameColumn(
                name: "pickupreasonkey",
                schema: "Fact",
                table: "Deployment",
                newName: "PickupReasonKey");

            migrationBuilder.RenameColumn(
                name: "patternareakey",
                schema: "Fact",
                table: "Deployment",
                newName: "PatternAreaKey");

            migrationBuilder.RenameColumn(
                name: "neighborhoodkey",
                schema: "Fact",
                table: "Deployment",
                newName: "NeighborhoodKey");

            migrationBuilder.RenameColumn(
                name: "location",
                schema: "Fact",
                table: "Deployment",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "lastseen",
                schema: "Fact",
                table: "Deployment",
                newName: "LastSeen");

            migrationBuilder.RenameColumn(
                name: "ineastportland",
                schema: "Fact",
                table: "Deployment",
                newName: "InEastPortland");

            migrationBuilder.RenameColumn(
                name: "firstseen",
                schema: "Fact",
                table: "Deployment",
                newName: "FirstSeen");

            migrationBuilder.RenameColumn(
                name: "endtime",
                schema: "Fact",
                table: "Deployment",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "enddatekey",
                schema: "Fact",
                table: "Deployment",
                newName: "EndDateKey");

            migrationBuilder.RenameColumn(
                name: "disabled",
                schema: "Fact",
                table: "Deployment",
                newName: "Disabled");

            migrationBuilder.RenameColumn(
                name: "companykey",
                schema: "Fact",
                table: "Deployment",
                newName: "CompanyKey");

            migrationBuilder.RenameColumn(
                name: "batterylevel",
                schema: "Fact",
                table: "Deployment",
                newName: "BatteryLevel");

            migrationBuilder.RenameColumn(
                name: "alternatekey",
                schema: "Fact",
                table: "Deployment",
                newName: "AlternateKey");

            migrationBuilder.RenameColumn(
                name: "allowedplacement",
                schema: "Fact",
                table: "Deployment",
                newName: "AllowedPlacement");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Fact",
                table: "Deployment",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_vehiclekey_startdatekey_starttime",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_VehicleKey_StartDateKey_StartTime");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_vehicletypekey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_VehicleTypeKey");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_startdatekey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_StartDateKey");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_placementreasonkey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_PlacementReasonKey");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_pickupreasonkey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_PickupReasonKey");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_patternareakey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_PatternAreaKey");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_neighborhoodkey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_NeighborhoodKey");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_location",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_Location");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_enddatekey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_EndDateKey");

            migrationBuilder.RenameIndex(
                name: "IX_deployment_companykey",
                schema: "Fact",
                table: "Deployment",
                newName: "IX_Deployment_CompanyKey");

            migrationBuilder.RenameColumn(
                name: "calendarkey",
                schema: "Fact",
                table: "Complaint",
                newName: "CalendarKey");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Fact",
                table: "Complaint",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_complaint_calendarkey",
                schema: "Fact",
                table: "Complaint",
                newName: "IX_Complaint_CalendarKey");

            migrationBuilder.RenameColumn(
                name: "calendarkey",
                schema: "Fact",
                table: "Collision",
                newName: "CalendarKey");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Fact",
                table: "Collision",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_collision_calendarkey",
                schema: "Fact",
                table: "Collision",
                newName: "IX_Collision_CalendarKey");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "VehicleType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "VehicleType",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_vehicletype_name",
                schema: "Dim",
                table: "VehicleType",
                newName: "IX_VehicleType_Name");

            migrationBuilder.RenameColumn(
                name: "typekey",
                schema: "Dim",
                table: "Vehicle",
                newName: "TypeKey");

            migrationBuilder.RenameColumn(
                name: "registered",
                schema: "Dim",
                table: "Vehicle",
                newName: "Registered");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "Vehicle",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "companykey",
                schema: "Dim",
                table: "Vehicle",
                newName: "CompanyKey");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "Vehicle",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_typekey",
                schema: "Dim",
                table: "Vehicle",
                newName: "IX_Vehicle_TypeKey");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_name",
                schema: "Dim",
                table: "Vehicle",
                newName: "IX_Vehicle_Name");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_companykey",
                schema: "Dim",
                table: "Vehicle",
                newName: "IX_Vehicle_CompanyKey");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "RemovalReason",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "RemovalReason",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_removalreason_name",
                schema: "Dim",
                table: "RemovalReason",
                newName: "IX_RemovalReason_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "PlacementReason",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "PlacementReason",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_placementreason_name",
                schema: "Dim",
                table: "PlacementReason",
                newName: "IX_PlacementReason_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "PaymentType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "PaymentType",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_paymenttype_name",
                schema: "Dim",
                table: "PaymentType",
                newName: "IX_PaymentType_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "PatternArea",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "geometry",
                schema: "Dim",
                table: "PatternArea",
                newName: "Geometry");

            migrationBuilder.RenameColumn(
                name: "alternatekey",
                schema: "Dim",
                table: "PatternArea",
                newName: "AlternateKey");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "PatternArea",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_patternarea_geometry",
                schema: "Dim",
                table: "PatternArea",
                newName: "IX_PatternArea_Geometry");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "Neighborhood",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "geometry",
                schema: "Dim",
                table: "Neighborhood",
                newName: "Geometry");

            migrationBuilder.RenameColumn(
                name: "alternatekey",
                schema: "Dim",
                table: "Neighborhood",
                newName: "AlternateKey");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "Neighborhood",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_neighborhood_geometry",
                schema: "Dim",
                table: "Neighborhood",
                newName: "IX_Neighborhood_Geometry");

            migrationBuilder.RenameIndex(
                name: "IX_neighborhood_alternatekey",
                schema: "Dim",
                table: "Neighborhood",
                newName: "IX_Neighborhood_AlternateKey");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Dim",
                table: "Company",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "Company",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_company_name",
                schema: "Dim",
                table: "Company",
                newName: "IX_Company_Name");

            migrationBuilder.RenameColumn(
                name: "year",
                schema: "Dim",
                table: "Calendar",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "weekday",
                schema: "Dim",
                table: "Calendar",
                newName: "Weekday");

            migrationBuilder.RenameColumn(
                name: "weekofyear",
                schema: "Dim",
                table: "Calendar",
                newName: "WeekOfYear");

            migrationBuilder.RenameColumn(
                name: "weekofmonth",
                schema: "Dim",
                table: "Calendar",
                newName: "WeekOfMonth");

            migrationBuilder.RenameColumn(
                name: "weekdayname",
                schema: "Dim",
                table: "Calendar",
                newName: "WeekDayName");

            migrationBuilder.RenameColumn(
                name: "monthyear",
                schema: "Dim",
                table: "Calendar",
                newName: "MonthYear");

            migrationBuilder.RenameColumn(
                name: "monthname",
                schema: "Dim",
                table: "Calendar",
                newName: "MonthName");

            migrationBuilder.RenameColumn(
                name: "month",
                schema: "Dim",
                table: "Calendar",
                newName: "Month");

            migrationBuilder.RenameColumn(
                name: "mmyyyy",
                schema: "Dim",
                table: "Calendar",
                newName: "MMYYYY");

            migrationBuilder.RenameColumn(
                name: "isweekend",
                schema: "Dim",
                table: "Calendar",
                newName: "IsWeekend");

            migrationBuilder.RenameColumn(
                name: "isholiday",
                schema: "Dim",
                table: "Calendar",
                newName: "IsHoliday");

            migrationBuilder.RenameColumn(
                name: "holidaytext",
                schema: "Dim",
                table: "Calendar",
                newName: "HolidayText");

            migrationBuilder.RenameColumn(
                name: "dayofyear",
                schema: "Dim",
                table: "Calendar",
                newName: "DayOfYear");

            migrationBuilder.RenameColumn(
                name: "day",
                schema: "Dim",
                table: "Calendar",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "date",
                schema: "Dim",
                table: "Calendar",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "key",
                schema: "Dim",
                table: "Calendar",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_calendar_date",
                schema: "Dim",
                table: "Calendar",
                newName: "IX_Calendar_Date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                schema: "Fact",
                table: "Trip",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deployment",
                schema: "Fact",
                table: "Deployment",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaint",
                schema: "Fact",
                table: "Complaint",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collision",
                schema: "Fact",
                table: "Collision",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleType",
                schema: "Dim",
                table: "VehicleType",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                schema: "Dim",
                table: "Vehicle",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemovalReason",
                schema: "Dim",
                table: "RemovalReason",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlacementReason",
                schema: "Dim",
                table: "PlacementReason",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentType",
                schema: "Dim",
                table: "PaymentType",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatternArea",
                schema: "Dim",
                table: "PatternArea",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Neighborhood",
                schema: "Dim",
                table: "Neighborhood",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                schema: "Dim",
                table: "Company",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calendar",
                schema: "Dim",
                table: "Calendar",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Company_CompanyKey",
                schema: "Dim",
                table: "Vehicle",
                column: "CompanyKey",
                principalSchema: "Dim",
                principalTable: "Company",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleType_TypeKey",
                schema: "Dim",
                table: "Vehicle",
                column: "TypeKey",
                principalSchema: "Dim",
                principalTable: "VehicleType",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Collision_Calendar_CalendarKey",
                schema: "Fact",
                table: "Collision",
                column: "CalendarKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Calendar_CalendarKey",
                schema: "Fact",
                table: "Complaint",
                column: "CalendarKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_Company_CompanyKey",
                schema: "Fact",
                table: "Deployment",
                column: "CompanyKey",
                principalSchema: "Dim",
                principalTable: "Company",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_Calendar_EndDateKey",
                schema: "Fact",
                table: "Deployment",
                column: "EndDateKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Deployment_PatternArea_PatternAreaKey",
                schema: "Fact",
                table: "Deployment",
                column: "PatternAreaKey",
                principalSchema: "Dim",
                principalTable: "PatternArea",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_RemovalReason_PickupReasonKey",
                schema: "Fact",
                table: "Deployment",
                column: "PickupReasonKey",
                principalSchema: "Dim",
                principalTable: "RemovalReason",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_PlacementReason_PlacementReasonKey",
                schema: "Fact",
                table: "Deployment",
                column: "PlacementReasonKey",
                principalSchema: "Dim",
                principalTable: "PlacementReason",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_Calendar_StartDateKey",
                schema: "Fact",
                table: "Deployment",
                column: "StartDateKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_Vehicle_VehicleKey",
                schema: "Fact",
                table: "Deployment",
                column: "VehicleKey",
                principalSchema: "Dim",
                principalTable: "Vehicle",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_VehicleType_VehicleTypeKey",
                schema: "Fact",
                table: "Deployment",
                column: "VehicleTypeKey",
                principalSchema: "Dim",
                principalTable: "VehicleType",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Company_CompanyKey",
                schema: "Fact",
                table: "Trip",
                column: "CompanyKey",
                principalSchema: "Dim",
                principalTable: "Company",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Calendar_EndDateKey",
                schema: "Fact",
                table: "Trip",
                column: "EndDateKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_PaymentType_PaymentAccessKey",
                schema: "Fact",
                table: "Trip",
                column: "PaymentAccessKey",
                principalSchema: "Dim",
                principalTable: "PaymentType",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_PaymentType_PaymentTypeKey",
                schema: "Fact",
                table: "Trip",
                column: "PaymentTypeKey",
                principalSchema: "Dim",
                principalTable: "PaymentType",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Calendar_StartDateKey",
                schema: "Fact",
                table: "Trip",
                column: "StartDateKey",
                principalSchema: "Dim",
                principalTable: "Calendar",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Vehicle_VehicleKey",
                schema: "Fact",
                table: "Trip",
                column: "VehicleKey",
                principalSchema: "Dim",
                principalTable: "Vehicle",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_VehicleType_VehicleTypeKey",
                schema: "Fact",
                table: "Trip",
                column: "VehicleTypeKey",
                principalSchema: "Dim",
                principalTable: "VehicleType",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
