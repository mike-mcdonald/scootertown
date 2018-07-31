using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;

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

            modelBuilder.Entity<Vehicle>(vehicle =>
            {
                vehicle.ToTable(storeOptions.Vehicle);

                vehicle.HasKey(x => x.Key);

                vehicle.Property(x => x.NavManKey);
                vehicle.Property(x => x.Name).HasMaxLength(200).IsRequired();
                vehicle.Property(x => x.Description).HasMaxLength(1000);
                vehicle.Property(x => x.PWNumber).HasMaxLength(8);
                vehicle.Property(x => x.FleetID).HasMaxLength(6);

                vehicle.HasIndex(x => x.Name).IsUnique();
                vehicle.HasIndex(x => x.NavManKey).IsUnique();

                vehicle.HasOne(x => x.Bureau).WithMany(x => x.Vehicles).OnDelete(DeleteBehavior.Restrict);
                vehicle.HasOne(x => x.Group).WithMany(x => x.Vehicles).OnDelete(DeleteBehavior.Restrict);
                vehicle.HasOne(x => x.Type).WithMany(x => x.Vehicles).OnDelete(DeleteBehavior.Restrict);
                vehicle.HasMany(x => x.Locations).WithOne(x => x.Vehicle).OnDelete(DeleteBehavior.Restrict);
                
            });

            modelBuilder.Entity<VehicleBureau>(bureau => 
            {
                bureau.ToTable(storeOptions.VehicleBureau);

                bureau.HasKey(x => x.Key);

                bureau.Property(x => x.NavManKey);
                bureau.Property(x => x.Name).HasMaxLength(200).IsRequired();
                bureau.Property(x => x.Description).HasMaxLength(1000);
                bureau.Property(x => x.Inactive).HasDefaultValue(false);

                bureau.HasIndex(x => x.Name).IsUnique();
                bureau.HasIndex(x => x.NavManKey).IsUnique();

                bureau.HasMany(x => x.ChildGroups).WithOne(x => x.Bureau).OnDelete(DeleteBehavior.Restrict);
                bureau.HasMany(x => x.Locations).WithOne(x => x.Bureau).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VehicleGroup>(group =>
            {
                group.ToTable(storeOptions.VehicleGroup);

                group.HasKey(x => x.Key);

                group.Property(x => x.NavManKey);
                group.Property(x => x.Name).HasMaxLength(200).IsRequired();
                group.Property(x => x.Description).HasMaxLength(1000);
                
                group.HasIndex(x => x.Name).IsUnique();
                group.HasIndex(x => x.NavManKey).IsUnique();

                group.HasMany(x => x.Children).WithOne(x => x.Parent).OnDelete(DeleteBehavior.Restrict);
                group.HasMany(x => x.Locations).WithOne(x => x.Group).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VehicleType>(type =>
            {
                type.ToTable(storeOptions.VehicleType);

                type.HasKey(x => x.Key);

                type.Property(x => x.NavManKey);
                type.Property(x => x.Name).HasMaxLength(200).IsRequired();
                type.Property(x => x.Description).HasMaxLength(1000);
                
                type.HasIndex(x => x.NavManKey).IsUnique();

                type.HasMany(x => x.Locations).WithOne(x => x.Type).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Calendar>(calendar => 
            {
                calendar.ToTable(storeOptions.Calendar);

                calendar.HasKey(x => x.DateKey);

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

                calendar.HasMany(x => x.Locations).WithOne(x => x.Date).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VehicleLocation>(location =>
            {
                location.ToTable(storeOptions.VehicleLocation);

                location.HasKey( x => x.Key);

                location.Property(x => x.Longitude).IsRequired();
                location.Property(x => x.Latitude).IsRequired();
                location.Property(x => x.Timestamp).IsRequired();
                location.Property(x => x.Speed);
                location.Property(x => x.Heading);
                location.Property(x => x.Odometer);

                location.HasOne(x => x.Date).WithMany(x => x.Locations).IsRequired().OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
