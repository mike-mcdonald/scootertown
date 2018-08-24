using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Integration.Models;
using PDX.PBOT.Scootertown.Integration.Services.Interfaces;

namespace PDX.PBOT.Scootertown.Integration.Services.Implementations
{
    public class TripService : ServiceBase<TripDTO, Trip>, ITripService
    {
        private readonly ITripRepository TripRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly INeighborhoodRepository NeighborhoodRepository;
        private readonly IPaymentTypeRepository PaymentTypeRepository;
        private readonly IVehicleRepository VehicleRepository;
        private readonly IVehicleTypeRepository VehicleTypeRepository;

        public TripService(
            ITripRepository tripRepository,
            ICalendarRepository calendarRepository,
            ICompanyRepository companyRepository,
            IPaymentTypeRepository paymentTypeRepository,
            IVehicleRepository vehicleRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            INeighborhoodRepository neighborhoodRepository
        ) : base(calendarRepository)
        {
            TripRepository = tripRepository;
            CompanyRepository = companyRepository;
            PaymentTypeRepository = paymentTypeRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
            NeighborhoodRepository = neighborhoodRepository;
        }

        public async Task<long> GetTotalTrips(string companyName) => await CompanyRepository.GetTripCount(companyName);

        public override async Task Save(Queue<TripDTO> items)
        {
            while (items.Count > 0)
            {
                var item = items.Dequeue();

                var existingTask = TripRepository.Find(item.AlternateKey);

                // Get the reference properties set up
                var companyTask = FindOrAdd<Company>(CompanyRepository, item.Company, new Company { Name = item.Company });
                var startDateTask = FindOrAddCalendar(item.StartTime);
                var endDateTask = FindOrAddCalendar(item.EndTime);
                var vehicleTask = FindOrAdd<Vehicle>(VehicleRepository, item.Vehicle, new Vehicle { Name = item.Vehicle });
                var vehicleTypeTask = FindOrAdd<VehicleType>(VehicleTypeRepository, item.VehicleType, new VehicleType { Key = item.VehicleType });
                var paymentTypeTask = FindOrAdd<PaymentType>(PaymentTypeRepository, item.PaymentType, new PaymentType { Key = item.PaymentType });
                var paymentAccessTask = FindOrAdd<PaymentType>(PaymentTypeRepository, item.PaymentAccess, new PaymentType { Key = item.PaymentAccess });

                var existing = await existingTask;
                var trip = existing ?? Mapper.Map<Trip>(item);
                
                var neighborhoodStartTask = NeighborhoodRepository.Find(trip.StartPoint);
                var neighborhoodEndTask = NeighborhoodRepository.Find(trip.EndPoint);

                trip.CompanyKey = (await companyTask).Key;
                trip.StartDateKey = (await startDateTask).Key;
                trip.EndDateKey = (await endDateTask).Key;
                trip.VehicleKey = (await vehicleTask).Key;
                trip.VehicleTypeKey = (await vehicleTypeTask).Key;
                trip.PaymentTypeKey = (await paymentTypeTask).Key;
                trip.PaymentAccessKey = (await paymentAccessTask).Key;

                trip.NeighborhoodStartKey = (await neighborhoodStartTask)?.Key;
                trip.NeighborhoodEndKey = (await neighborhoodEndTask)?.Key;

                // add it to the database
                await existing == null ? TripRepository.Add(trip, false) : TripRepository.Update(trip, false);
            }

            await TripRepository.SaveChanges();
        }
    }
}
