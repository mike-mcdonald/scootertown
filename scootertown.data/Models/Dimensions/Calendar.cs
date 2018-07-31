using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
	public class Calendar
	{
		public int DateKey { get; set; }
		public DateTime Date { get; set; }
		public byte Day { get; set; }
		public byte Weekday { get; set; }
		public string WeekDayName { get; set; }
		public bool IsWeekend { get; set; }
		public bool IsHoliday { get; set; }
		public string HolidayText { get; set; }
		public short DayOfYear { get; set; }
		public byte WeekOfMonth { get; set; }
		public byte WeekOfYear { get; set; }
		public byte Month { get; set; }
		public string MonthName { get; set; }
		public short Year { get; set; }
		public string MMYYYY { get; set; }
		public string MonthYear { get; set; }

        public List<VehicleLocation> Locations { get; set; }
	} 
}

