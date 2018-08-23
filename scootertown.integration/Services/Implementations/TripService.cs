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

        public override async Task Save(List<TripDTO> items)
        {
            var trips = new List<Trip>();

            foreach (var item in items)
            {
                if (TripRepository.Find(item.AlternateKey) != null)
                {
                    // Use automapper for the original properties
                    var trip = Mapper.Map<Trip>(item);

                    // Get the reference properties set up
                    trip.CompanyKey = (await FindOrAdd<Company>(CompanyRepository, item.Company, new Company { Name = item.Company })).Key;
                    trip.StartDateKey = (await FindOrAddCalendar(item.StartTime)).Key;
                    trip.EndDateKey = (await FindOrAddCalendar(item.EndTime)).Key;
                    trip.VehicleKey = (await FindOrAdd<Vehicle>(VehicleRepository, item.Vehicle, new Vehicle { Name = item.Vehicle })).Key;
                    trip.VehicleTypeKey = (await FindOrAdd<VehicleType>(VehicleTypeRepository, item.VehicleType, new VehicleType { Key = item.VehicleType })).Key;
                    trip.PaymentTypeKey = (await FindOrAdd<PaymentType>(PaymentTypeRepository, item.PaymentType, new PaymentType { Key = item.PaymentType })).Key;
                    trip.PaymentAccessKey = (await FindOrAdd<PaymentType>(PaymentTypeRepository, item.PaymentAccess, new PaymentType { Key = item.PaymentAccess })).Key;

                    trip.NeighborhoodStartKey = (await NeighborhoodRepository.Find(trip.StartPoint))?.Key;
                    trip.NeighborhoodEndKey = (await NeighborhoodRepository.Find(trip.EndPoint))?.Key;

                    // add it to the database
                    await TripRepository.Add(trip);
                }
            }
        }
    }
}
