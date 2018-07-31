using System;
using System.Threading.Tasks;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Repositories.Interfaces
{
	public interface ICalendarRepository
	{
		Task<Calendar> Find(DateTime date);
		Task<Calendar> Add(DateTime date); 
	}
}