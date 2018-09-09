using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PDX.PBOT.Scootertown.API.Models;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;

namespace PDX.PBOT.Scootertown.API.Controllers
{
    [Route("api/[controller]")]
    public class ComplaintController : Controller
    {
        private readonly ILogger Logger;
        private readonly IComplaintRepository ComplaintRepository;
        private readonly ICalendarRepository CalendarRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly IVehicleRepository VehicleRepository;

        public ComplaintController(
            ILogger<ComplaintController> logger,
            IComplaintRepository complaintRepository,
            ICalendarRepository calendarRepository,
            ICompanyRepository companyRepository,
            IVehicleRepository vehicleRepository
        )
        {
            Logger = logger;

            ComplaintRepository = complaintRepository;
            CalendarRepository = calendarRepository;
            CompanyRepository = companyRepository;
            VehicleRepository = vehicleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ComplaintDTO value)
        {
            var now = DateTime.Now;

            try
            {
                var fillTask = FillComplaint(value);
                var existingTask = ComplaintRepository.Find(value.SubmittedDate, value.ComplaintDetails);

                var complaint = await fillTask;

                var existing = await existingTask;

                complaint.Key = existing?.Key ?? 0;
                complaint.FirstSeen =
                    existing != null && existing.FirstSeen != DateTime.MinValue ? existing.FirstSeen : now;
                complaint.LastSeen = now;

                var upsertTask = existing == null ? ComplaintRepository.Add(complaint) : ComplaintRepository.Update(complaint);

                return Ok(Mapper.Map<ComplaintDTO>(await upsertTask));
            }
            catch (Exception e)
            {
                Logger.LogError("Error adding complaint:\n{message}", e.Message);
                return BadRequest(e.ToString());
            }
        }

        private async Task<Complaint> FillComplaint(ComplaintDTO value)
        {
            var complaint = Mapper.Map<Complaint>(value);

            var calendarTask = CalendarRepository.Find(value.SubmittedDate);
            var companyTask = CompanyRepository.Find(value.CompanyName);
            var vehicleTask = VehicleRepository.Find(value.VehicleName);

            complaint.SubmittedDateKey = (await calendarTask).Key;
            complaint.CompanyKey = (await companyTask).Key;

            var vehicle = await vehicleTask ?? await VehicleRepository.Add(new Vehicle { Name = value.VehicleName });
            complaint.VehicleKey = vehicle.Key;

            return complaint;
        }
    }
}
