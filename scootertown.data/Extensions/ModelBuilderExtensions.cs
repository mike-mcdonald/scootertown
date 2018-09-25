
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Models.Bridges;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PDX.PBOT.Scootertown.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration)
            where TEntity : class
        {
            return string.IsNullOrWhiteSpace(configuration.Schema) ? entityTypeBuilder.ToTable(configuration.Name) : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }

        public static void ConfigureContext(this ModelBuilder modelBuilder, VehicleStoreOptions storeOptions)
        {
            if (!string.IsNullOrWhiteSpace(storeOptions.DefaultSchema)) modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema);

            modelBuilder.HasPostgresExtension("postgis");

            modelBuilder.Entity<Models.Bridges.StreetSegmentGroup>(group =>
            {
                group.ToTable(storeOptions.BridgeStreetSegmentGroup);

                group.HasKey(x => new { x.StreetSegmentGroupKey, x.StreetSegmentKey });

                group.HasOne(x => x.StreetSegment).WithMany(x => x.Bridges).HasForeignKey(x => x.StreetSegmentKey);
                group.HasOne(x => x.SegmentGroup).WithMany(x => x.Bridges).HasForeignKey(x => x.StreetSegmentGroupKey);
            });

            modelBuilder.Entity<Models.Bridges.BicyclePathGroup>(group =>
            {
                group.ToTable(storeOptions.BridgeBicyclePathGroup);

                group.HasKey(x => new { x.BicyclePathGroupKey, x.BicyclePathKey });

                group.HasOne(x => x.BicyclePath).WithMany(x => x.Bridges).HasForeignKey(x => x.BicyclePathKey);
                group.HasOne(x => x.PathGroup).WithMany(x => x.Bridges).HasForeignKey(x => x.BicyclePathGroupKey);
            });

            modelBuilder.Entity<BicyclePath>(path =>
            {
                path.ToTable(storeOptions.BicyclePath);

                path.HasKey(x => x.Key);

                path.Property(x => x.Name).HasMaxLength(200).IsRequired();
                path.Property(x => x.AlternateKey).HasMaxLength(32).IsRequired();
                path.Property(x => x.Geometry);
                path.Property(x => x.X);
                path.Property(x => x.Y);
                path.Property(x => x.Buffer);

                path.HasIndex(x => x.AlternateKey).IsUnique();
                path.HasIndex(x => x.Geometry).ForNpgsqlHasMethod("gist");

                path.HasMany(x => x.Bridges).WithOne(x => x.BicyclePath).HasForeignKey(x => x.BicyclePathKey);
            });

            modelBuilder.Entity<Models.Dimensions.BicyclePathGroup>(group =>
            {
                group.ToTable(storeOptions.DimBicyclePathGroup);

                group.HasKey(x => x.Key);

                group.HasMany(x => x.Bridges).WithOne(x => x.PathGroup).HasForeignKey(x => x.BicyclePathGroupKey);
                group.HasOne(x => x.Trip).WithOne(x => x.BicyclePathGroup).HasForeignKey<Trip>(x => x.BicyclePathGroupKey);
            });

            modelBuilder.Entity<Calendar>(calendar =>
            {
                calendar.ToTable(storeOptions.Calendar);

                calendar.HasKey(x => x.Key);

                calendar.Property(x => x.Date).IsRequired().HasColumnType("Date");
                calendar.Property(x => x.Day);
                calendar.Property(x => x.WeekDayName);
                calendar.Property(x => x.IsWeekend);
                calendar.Property(x => x.IsHoliday);
                calendar.Property(x => x.HolidayText);
                calendar.Property(x => x.DayOfYear);
                calendar.Property(x => x.WeekOfMonth);
                calendar.Property(x => x.WeekOfYear);
                calendar.Property(x => x.Month);
                calendar.Property(x => x.MonthName);
                calendar.Property(x => x.Year);
                calendar.Property(x => x.MMYYYY);
                calendar.Property(x => x.MonthYear);

                calendar.HasIndex(x => x.Date).IsUnique();

                calendar.HasMany(x => x.TripsStarted).WithOne(x => x.StartDate);
                calendar.HasMany(x => x.TripsEnded).WithOne(x => x.EndDate);
            });

            modelBuilder.Entity<Company>(company =>
            {
                company.ToTable(storeOptions.Company);

                company.HasKey(x => x.Key);

                company.Property(x => x.Name).HasMaxLength(200).IsRequired();

                company.HasIndex(x => x.Name).IsUnique();

                company.HasMany(x => x.Vehicles).WithOne(x => x.Company);
                company.HasMany(x => x.Trips).WithOne(x => x.Company);
                company.HasMany(x => x.Deployments).WithOne(x => x.Company);
            });

            modelBuilder.Entity<ComplaintType>(type =>
            {
                type.ToTable(storeOptions.ComplaintType);

                type.HasKey(x => x.Key);

                type.Property(x => x.Name).HasMaxLength(200).IsRequired();

                type.HasIndex(x => x.Name).IsUnique();

                type.HasMany(x => x.Complaints).WithOne(x => x.ComplaintType).HasForeignKey(x => x.ComplaintTypeKey);
            });

            modelBuilder.Entity<Neighborhood>(neighborhood =>
            {
                neighborhood.ToTable(storeOptions.Neighborhood);

                neighborhood.HasKey(x => x.Key);

                neighborhood.Property(x => x.Name).HasMaxLength(200).IsRequired();
                neighborhood.Property(x => x.AlternateKey).IsRequired();
                neighborhood.Property(x => x.Geometry);

                neighborhood.HasIndex(x => x.AlternateKey).IsUnique();
                neighborhood.HasIndex(x => x.Geometry).ForNpgsqlHasMethod("gist");

                neighborhood.HasMany(x => x.Deployments).WithOne(x => x.Neighborhood).HasForeignKey(x => x.NeighborhoodKey);
                neighborhood.HasMany(x => x.TripsStarted).WithOne(x => x.NeighborhoodStart).HasForeignKey(x => x.NeighborhoodStartKey);
                neighborhood.HasMany(x => x.TripsEnded).WithOne(x => x.NeighborhoodEnd).HasForeignKey(x => x.NeighborhoodEndKey);
            });

            modelBuilder.Entity<PatternArea>(patternArea =>
           {
               patternArea.ToTable(storeOptions.PatternArea);

               patternArea.HasKey(x => x.Key);

               patternArea.Property(x => x.Name).HasMaxLength(200).IsRequired();
               patternArea.Property(x => x.Geometry);

               patternArea.HasIndex(x => x.Geometry).ForNpgsqlHasMethod("gist");

               patternArea.HasMany(x => x.Deployments).WithOne(x => x.PatternArea).HasForeignKey(x => x.PatternAreaKey);
               patternArea.HasMany(x => x.TripsStarted).WithOne(x => x.PatternAreaStart).HasForeignKey(x => x.PatternAreaStartKey);
               patternArea.HasMany(x => x.TripsEnded).WithOne(x => x.PatternAreaEnd).HasForeignKey(x => x.PatternAreaEndKey);
           });

            modelBuilder.Entity<PaymentType>(type =>
            {
                type.ToTable(storeOptions.PaymentType);

                type.HasKey(x => x.Key);

                type.Property(x => x.Name).HasMaxLength(200).IsRequired();

                type.HasIndex(x => x.Name).IsUnique();

                type.HasMany(x => x.TripsPayType).WithOne(x => x.PaymentType);
                type.HasMany(x => x.TripsPayAccess).WithOne(x => x.PaymentAccess);
            });

            modelBuilder.Entity<PlacementReason>(reason =>
            {
                reason.ToTable(storeOptions.PlacementReason);

                reason.HasKey(x => x.Key);

                reason.Property(x => x.Name).HasMaxLength(200).IsRequired();

                reason.HasIndex(x => x.Name).IsUnique();

                reason.HasMany(x => x.Deployments).WithOne(x => x.PlacementReason);
            });

            modelBuilder.Entity<RemovalReason>(reason =>
            {
                reason.ToTable(storeOptions.RemovalReason);

                reason.HasKey(x => x.Key);

                reason.Property(x => x.Name).HasMaxLength(200).IsRequired();

                reason.HasIndex(x => x.Name).IsUnique();

                reason.HasMany(x => x.Deployments).WithOne(x => x.PickupReason);
            });

            modelBuilder.Entity<Status>(status =>
            {
                status.ToTable(storeOptions.Status);

                status.HasKey(x => x.Key);

                status.Property(x => x.Name).HasMaxLength(50).IsRequired();

                status.HasIndex(x => x.Name).IsUnique();

                status.HasMany(x => x.Collisions).WithOne(x => x.ClaimStatus).HasForeignKey(x => x.ClaimStatusKey);
            });

            modelBuilder.Entity<StreetSegment>(segment =>
            {
                segment.ToTable(storeOptions.StreetSegment);

                segment.HasKey(x => x.Key);

                segment.Property(x => x.Name).HasMaxLength(200).IsRequired();
                segment.Property(x => x.AlternateKey).HasMaxLength(32).IsRequired();
                segment.Property(x => x.Geometry);
                segment.Property(x => x.X);
                segment.Property(x => x.Y);
                segment.Property(x => x.Buffer);

                segment.HasIndex(x => x.AlternateKey).IsUnique();
                segment.HasIndex(x => x.Geometry).ForNpgsqlHasMethod("gist");

                segment.HasMany(x => x.Bridges).WithOne(x => x.StreetSegment).HasForeignKey(x => x.StreetSegmentKey);
            });

            modelBuilder.Entity<Models.Dimensions.StreetSegmentGroup>(group =>
            {
                group.ToTable(storeOptions.DimStreetSegmentGroup);

                group.HasKey(x => x.Key);

                group.HasMany(x => x.Bridges).WithOne(x => x.SegmentGroup).HasForeignKey(x => x.StreetSegmentGroupKey);
                group.HasOne(x => x.Trip).WithOne(x => x.StreetSegmentGroup).HasForeignKey<Trip>(x => x.StreetSegmentGroupKey);
            });

            modelBuilder.Entity<Vehicle>(vehicle =>
            {
                vehicle.ToTable(storeOptions.Vehicle);

                vehicle.HasKey(x => x.Key);

                vehicle.Property(x => x.Name).HasMaxLength(200).IsRequired();
                vehicle.Property(x => x.Registered);

                vehicle.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<VehicleType>(type =>
            {
                type.ToTable(storeOptions.VehicleType);

                type.HasKey(x => x.Key);

                type.Property(x => x.Name).HasMaxLength(200).IsRequired();

                type.HasIndex(x => x.Name).IsUnique();

                type.HasMany(x => x.Trips).WithOne(x => x.VehicleType);
                type.HasMany(x => x.Deployments).WithOne(x => x.VehicleType);
            });

            modelBuilder.Entity<Collision>(collision =>
            {
                collision.ToTable(storeOptions.Collision);

                collision.HasKey(x => x.Key);

                collision.Property(x => x.Time).IsRequired();
                collision.Property(x => x.FirstSeen);
                collision.Property(x => x.LastSeen);
                collision.Property(x => x.OtherUser);
                collision.Property(x => x.Helmet);
                collision.Property(x => x.Location);
                collision.Property(x => x.X);
                collision.Property(x => x.Y);
                collision.Property(x => x.Citation);
                collision.Property(x => x.CitationDetails);
                collision.Property(x => x.Injury);
                collision.Property(x => x.StateReport);
                collision.Property(x => x.InternalReports).HasColumnName("reports");

                // reference properties
                collision.Property(x => x.DateKey).IsRequired();
                collision.Property(x => x.TripKey);
                collision.Property(x => x.OtherVehicleTypeKey);
                collision.Property(x => x.ClaimStatusKey);

                collision.HasIndex(x => new { x.DateKey, x.Time }).IsUnique();
                collision.HasIndex(x => x.Location);

                collision.HasOne(x => x.Date).WithMany(x => x.Collisions).HasForeignKey(x => x.DateKey);
                collision.HasOne(x => x.Trip).WithMany(x => x.Collisions).HasForeignKey(x => x.TripKey);
                collision.HasOne(x => x.OtherVehicleType).WithMany(x => x.Collisions).HasForeignKey(x => x.OtherVehicleTypeKey);
                collision.HasOne(x => x.ClaimStatus).WithMany(x => x.Collisions).HasForeignKey(x => x.ClaimStatusKey);
            });

            modelBuilder.Entity<Complaint>(complaint =>
            {
                complaint.ToTable(storeOptions.Complaint);

                complaint.HasKey(x => x.Key);

                complaint.Property(x => x.SubmittedTime).IsRequired();
                complaint.Property(x => x.FirstSeen);
                complaint.Property(x => x.LastSeen);
                complaint.Property(x => x.Location);
                complaint.Property(x => x.X);
                complaint.Property(x => x.Y);
                complaint.Property(x => x.ComplaintDetails);
                complaint.Property(x => x.InternalComplaints).HasColumnName("complaints");

                // references
                complaint.Property(x => x.SubmittedDateKey).IsRequired();
                complaint.Property(x => x.VehicleKey);
                complaint.Property(x => x.CompanyKey).IsRequired();
                complaint.Property(x => x.VehicleTypeKey);
                complaint.Property(x => x.ComplaintTypeKey);

                complaint.HasIndex(x => x.Location);
                complaint.HasIndex(x => new { x.SubmittedDateKey, x.SubmittedTime, x.ComplaintDetails });

                complaint.HasOne(x => x.SubmittedDate).WithMany(x => x.Complaints).HasForeignKey(x => x.SubmittedDateKey);
                complaint.HasOne(x => x.Vehicle).WithMany(x => x.Complaints).HasForeignKey(x => x.VehicleKey);
                complaint.HasOne(x => x.Company).WithMany(x => x.Complaints).HasForeignKey(x => x.CompanyKey);
                complaint.HasOne(x => x.VehicleType).WithMany(x => x.Complaints).HasForeignKey(x => x.VehicleTypeKey);
                complaint.HasOne(x => x.ComplaintType).WithMany(x => x.Complaints).HasForeignKey(x => x.ComplaintTypeKey);
            });

            modelBuilder.Entity<Deployment>(deployment =>
            {
                deployment.ToTable(storeOptions.Deployment);

                deployment.HasKey(x => x.Key);

                deployment.Property(x => x.StartTime);
                deployment.Property(x => x.EndTime);
                deployment.Property(x => x.FirstSeen);
                deployment.Property(x => x.LastSeen);
                deployment.Property(x => x.Location);
                deployment.Property(x => x.X);
                deployment.Property(x => x.Y);
                deployment.Property(x => x.BatteryLevel);
                deployment.Property(x => x.AllowedPlacement);
                deployment.Property(x => x.Reserved);
                deployment.Property(x => x.Disabled);

                // Foreign keys
                deployment.Property(x => x.VehicleKey);
                deployment.Property(x => x.CompanyKey);
                deployment.Property(x => x.VehicleTypeKey);
                deployment.Property(x => x.StartDateKey);
                deployment.Property(x => x.EndDateKey);
                deployment.Property(x => x.PlacementReasonKey);
                deployment.Property(x => x.PickupReasonKey);
                deployment.Property(x => x.NeighborhoodKey);

                // Indicies
                deployment.HasIndex(x => new { x.VehicleKey, x.StartDateKey, x.StartTime }).IsUnique();
                deployment.HasIndex(x => x.Location);

                // Relationships
                deployment.HasOne(x => x.Vehicle).WithMany(x => x.Deployments).HasForeignKey(x => x.VehicleKey);
                deployment.HasOne(x => x.Company).WithMany(x => x.Deployments).HasForeignKey(x => x.CompanyKey);
                deployment.HasOne(x => x.VehicleType).WithMany(x => x.Deployments).HasForeignKey(x => x.VehicleTypeKey);
                deployment.HasOne(x => x.StartDate).WithMany(x => x.DeploymentsStarted).HasForeignKey(x => x.StartDateKey);
                deployment.HasOne(x => x.EndDate).WithMany(x => x.DeploymentsEnded).HasForeignKey(x => x.EndDateKey);
                deployment.HasOne(x => x.PlacementReason).WithMany(x => x.Deployments).HasForeignKey(x => x.PlacementReasonKey);
                deployment.HasOne(x => x.PickupReason).WithMany(x => x.Deployments).HasForeignKey(x => x.PickupReasonKey);
                deployment.HasOne(x => x.Neighborhood).WithMany(x => x.Deployments).HasForeignKey(x => x.NeighborhoodKey);
            });

            modelBuilder.Entity<Trip>(trip =>
            {
                trip.ToTable(storeOptions.Trip);

                trip.HasKey(x => x.Key);

                // Original properties
                trip.Property(x => x.AlternateKey).HasMaxLength(50).IsRequired();
                trip.Property(x => x.StartTime);
                trip.Property(x => x.EndTime);
                trip.Property(x => x.FirstSeen);
                trip.Property(x => x.LastSeen);
                trip.Property(x => x.StartPoint);
                trip.Property(x => x.StartX);
                trip.Property(x => x.StartY);
                trip.Property(x => x.EndPoint);
                trip.Property(x => x.EndX);
                trip.Property(x => x.EndY);
                trip.Property(x => x.Route);
                trip.Property(x => x.Duration);
                trip.Property(x => x.Distance);
                trip.Property(x => x.Accuracy);
                trip.Property(x => x.SampleRate);
                trip.Property(x => x.MaxSpeed);
                trip.Property(x => x.AverageSpeed);
                trip.Property(x => x.StandardCost);
                trip.Property(x => x.ActualCost);
                trip.Property(x => x.ParkingVerification);

                // Foreign keys
                trip.Property(x => x.VehicleKey);
                trip.Property(x => x.CompanyKey);
                trip.Property(x => x.VehicleTypeKey);
                trip.Property(x => x.StartDateKey);
                trip.Property(x => x.EndDateKey);
                trip.Property(x => x.PaymentTypeKey);
                trip.Property(x => x.PaymentAccessKey);
                trip.Property(x => x.NeighborhoodStartKey);
                trip.Property(x => x.NeighborhoodEndKey);
                trip.Property(x => x.StreetSegmentGroupKey);
                trip.Property(x => x.BicyclePathGroupKey);

                // Indicies
                trip.HasIndex(x => x.AlternateKey).IsUnique();
                trip.HasIndex(x => new { x.VehicleKey, x.StartDateKey, x.StartTime }).IsUnique();
                trip.HasIndex(x => x.StartPoint);
                trip.HasIndex(x => x.EndPoint);
                // // gist is a bounding box index
                trip.HasIndex(x => x.Route).ForNpgsqlHasMethod("gist");

                // Relationships
                trip.HasOne(x => x.Company).WithMany(x => x.Trips).HasForeignKey(x => x.CompanyKey);
                trip.HasOne(x => x.StartDate).WithMany(x => x.TripsStarted).HasForeignKey(x => x.StartDateKey);
                trip.HasOne(x => x.EndDate).WithMany(x => x.TripsEnded).HasForeignKey(x => x.EndDateKey);
                trip.HasOne(x => x.PaymentType).WithMany(x => x.TripsPayType).HasForeignKey(x => x.PaymentTypeKey);
                trip.HasOne(x => x.PaymentAccess).WithMany(x => x.TripsPayAccess).HasForeignKey(x => x.PaymentAccessKey);
                trip.HasOne(x => x.Vehicle).WithMany(x => x.Trips).HasForeignKey(x => x.VehicleKey);
                trip.HasOne(x => x.VehicleType).WithMany(x => x.Trips).HasForeignKey(x => x.VehicleTypeKey);
                trip.HasOne(x => x.NeighborhoodStart).WithMany(x => x.TripsStarted).HasForeignKey(x => x.NeighborhoodStartKey);
                trip.HasOne(x => x.NeighborhoodEnd).WithMany(x => x.TripsEnded).HasForeignKey(x => x.NeighborhoodEndKey);
                trip.HasOne(x => x.StreetSegmentGroup).WithOne(x => x.Trip).HasForeignKey<Trip>(x => x.StreetSegmentGroupKey);
                trip.HasOne(x => x.BicyclePathGroup).WithOne(x => x.Trip).HasForeignKey<Trip>(x => x.BicyclePathGroupKey);
            });

            // transform everything to lowercase
            // PostgreSQL prefers everything lowercase by default
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .ToList()
                .ForEach(p => p.Relational().ColumnName = p.Name.ToLower());
        }

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company { Key = 1, Name = "Bird" },
                new Company { Key = 2, Name = "Lime" },
                new Company { Key = 3, Name = "Skip" },
                new Company { Key = 4, Name = "CycleHops" }
            );

            modelBuilder.Entity<ComplaintType>().HasData(
                new ComplaintType { Key = 1, Name = "Device found blocking pedestrian right-of-way" },
                new ComplaintType { Key = 2, Name = "Device found blocking bike-path" },
                new ComplaintType { Key = 3, Name = "Device found inside or blocking access to a building" },
                new ComplaintType { Key = 4, Name = "Device found blocking MAX or Streetcar tracks" },
                new ComplaintType { Key = 5, Name = "Device found blocking vehicle travel lane" },
                new ComplaintType { Key = 6, Name = "Device listed as available, but physically inaccessible" },
                new ComplaintType { Key = 7, Name = "Unpermitted company, vehicle" },
                new ComplaintType { Key = 8, Name = "Unsafe or unsanitary vehicle, damaged or missing equipment, vehicle in disrepair" },
                new ComplaintType { Key = 9, Name = "Fare greater than expected, extra charges, or un-identifiable charges added to fare" },
                new ComplaintType { Key = 10, Name = "User not wearing a helmet" },
                new ComplaintType { Key = 11, Name = "User observed riding on sidewalk" },
                new ComplaintType { Key = 12, Name = "Other" }
            );

            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType { Key = 1, Name = "Phone scan" },
                new PaymentType { Key = 2, Name = "Phone text" }
            );

            modelBuilder.Entity<PlacementReason>().HasData(
                new PlacementReason { Key = 1, Name = "User" },
                new PlacementReason { Key = 2, Name = "Rebalancing" }
            );

            modelBuilder.Entity<RemovalReason>().HasData(
                new RemovalReason { Key = 1, Name = "User" },
                new RemovalReason { Key = 2, Name = "Rebalancing" },
                new RemovalReason { Key = 3, Name = "Out of service area" },
                new RemovalReason { Key = 4, Name = "Maintenance" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { Key = 1, Name = "Open" },
                new Status { Key = 2, Name = "Closed" }
            );

            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType { Key = 1, Name = "Electric scooter" },
                new VehicleType { Key = 2, Name = "Electric bicycle" },
                new VehicleType { Key = 3, Name = "Scooter" },
                new VehicleType { Key = 4, Name = "Bicycle" },
                new VehicleType { Key = 5, Name = "None-Pedestrian" },
                new VehicleType { Key = 6, Name = "Motor vehicle" },
                new VehicleType { Key = 7, Name = "AV Motor vehicle" },
                new VehicleType { Key = 8, Name = "TNC Private-for-hire vehicle" },
                new VehicleType { Key = 9, Name = "AV Private-for-hire vehicle" },
                new VehicleType { Key = 10, Name = "Taxi" },
                new VehicleType { Key = 11, Name = "Pedicab" },
                new VehicleType { Key = 12, Name = "Bus" },
                new VehicleType { Key = 13, Name = "Motorcycle" },
                new VehicleType { Key = 14, Name = "Personal Assisted Mobility Device" },
                new VehicleType { Key = 15, Name = "Private or public agency transit vehicle" },
                new VehicleType { Key = 16, Name = "Other" }
            );

            var calendars = new List<Calendar>();
            var start = new DateTime(2018, 1, 1);
            var end = new DateTime(2030, 12, 31);
            for (int i = 0; i <= (end - start).Days; i++)
            {
                var day = start.AddDays(i);
                var calendar = new Calendar(day);
                calendar.Key = i + 1;
                calendars.Add(calendar);
            }
            modelBuilder.Entity<Calendar>().HasData(calendars.ToArray());
        }
    }
}
