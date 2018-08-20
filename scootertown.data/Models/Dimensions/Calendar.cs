using System;
using System.Collections.Generic;
using System.Linq;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Models.Dimensions
{
    public class Calendar
    {
        public int Key { get; set; }
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

        public List<Deployment> DeploymentsStarted { get; set; }
        public List<Deployment> DeploymentsEnded { get; set; }
        public List<Trip> TripsStarted { get; set; }
        public List<Trip> TripsEnded { get; set; }
        public List<Collision> Collisions { get; set; }
        public List<Complaint> Complaints { get; set; }

        public Calendar() { }
        public Calendar(DateTime date)
        {
            Date = date.Date;
            Day = (byte)date.Day;
            Weekday = (byte)date.DayOfWeek;
            WeekDayName = date.DayOfWeek.ToString();
            IsWeekend = getIsWeekend(date);
            IsHoliday = getIsUSHoliday(date);
            DayOfYear = (short)getDayOfYear(date);
            WeekOfMonth = (byte)getWeekOfMonth(date);
            WeekOfYear = (byte)getWeekOfYear(date);
            Month = (byte)date.Month;
            MonthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
            Year = (short)date.Year;
            MMYYYY = Month.ToString("00") + Year.ToString("0000");
            MonthYear = MonthName + Year;
        }

        private bool getIsUSHoliday(DateTime date)
        {
            return getHolidayList(date.Year).Count(r => r.Date == date) > 0;
        }
        private bool getIsWeekend(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday);
        }
        private int getQuarterOfYear(DateTime date)
        {
            return (int)Math.Floor(((decimal)date.Month + 2) / 3);
        }

        private int getDayOfYear(DateTime date)
        {
            System.Globalization.DateTimeFormatInfo dfi = System.Globalization.DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;
            return cal.GetDayOfYear(date);
        }

        private int getWeekOfMonth(DateTime date)
        {
            DateTime first = new DateTime(date.Year, date.Month, 1);
            return getWeekOfYear(date) - getWeekOfYear(first) + 1;
        }

        private int getWeekOfYear(DateTime date)
        {
            System.Globalization.DateTimeFormatInfo dfi = System.Globalization.DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        //http://stackoverflow.com/questions/18326446/how-to-calculate-holidays-for-the-usa
        // slightly modified for my readability
        // begin...
        private class Holiday
        {
            public string HolidayName { get; set; }
            public DateTime Date { get; set; }
            public Holiday(string holidayName, DateTime date)
            {
                HolidayName = holidayName;
                Date = date;
            }
        }

        // generate holiday list for a given year
        //   http://www.usa.gov/citizens/holidays.shtml      
        //   http://archive.opm.gov/operating_status_schedules/fedhol/2013.asp
        private static List<Holiday> getHolidayList(int vYear)
        {
            int FirstWeek = 1;
            int SecondWeek = 2;
            int ThirdWeek = 3;
            int FourthWeek = 4;
            int LastWeek = 5;

            List<Holiday> HolidayList = new List<Holiday>();

            // New Year's Day            Jan 1
            HolidayList.Add(new Holiday("NewYears", new DateTime(vYear, 1, 1)));
            // Martin Luther King, Jr. third Mon in Jan
            HolidayList.Add(new Holiday("MLK", GetNthDayOfNthWeek(new DateTime(vYear, 1, 1), DayOfWeek.Monday, ThirdWeek)));
            // Washington's Birthday third Mon in Feb
            HolidayList.Add(new Holiday("WashingtonsBDay", GetNthDayOfNthWeek(new DateTime(vYear, 2, 1), DayOfWeek.Monday, ThirdWeek)));
            // Memorial Day          last Mon in May
            HolidayList.Add(new Holiday("MemorialDay", GetNthDayOfNthWeek(new DateTime(vYear, 5, 1), DayOfWeek.Monday, LastWeek)));
            // Independence Day      July 4
            HolidayList.Add(new Holiday("IndependenceDay", new DateTime(vYear, 7, 4)));
            // Labor Day             first Mon in Sept
            HolidayList.Add(new Holiday("LaborDay", GetNthDayOfNthWeek(new DateTime(vYear, 9, 1), DayOfWeek.Monday, FirstWeek)));
            // Columbus Day          second Mon in Oct
            HolidayList.Add(new Holiday("Columbus", GetNthDayOfNthWeek(new DateTime(vYear, 10, 1), DayOfWeek.Monday, SecondWeek)));
            // Veterans Day          Nov 11
            HolidayList.Add(new Holiday("Veterans", new DateTime(vYear, 11, 11)));
            // Thanksgiving Day      fourth Thur in Nov
            HolidayList.Add(new Holiday("Thanksgiving", GetNthDayOfNthWeek(new DateTime(vYear, 11, 1), DayOfWeek.Thursday, FourthWeek)));
            // Christmas Day         Dec 25
            HolidayList.Add(new Holiday("Christmas", new DateTime(vYear, 12, 25)));
            //saturday holidays are moved to Fri; Sun to Mon
            foreach (var holiday in HolidayList)
            {
                if (holiday.Date.DayOfWeek == DayOfWeek.Saturday)
                    holiday.Date = holiday.Date.AddDays(-1);
                if (holiday.Date.DayOfWeek == DayOfWeek.Sunday)
                    holiday.Date = holiday.Date.AddDays(1);
            }
            //return
            return HolidayList;
        }

        //specify which day of which week of a month and this function will get the date
        //this function uses the month and year of the date provided
        private static System.DateTime GetNthDayOfNthWeek(DateTime dt, DayOfWeek dayofWeek, int WhichWeek)
        {
            //get first day of the given date
            System.DateTime dtFirst = new DateTime(dt.Year, dt.Month, 1);
            //get first DayOfWeek of the month
            System.DateTime dtRet = dtFirst.AddDays(6 - (int)dtFirst.AddDays(-1 * ((int)dayofWeek + 1)).DayOfWeek);
            //get which week
            dtRet = dtRet.AddDays((WhichWeek - 1) * 7);
            //if day is past end of month then adjust backwards a week
            if (dtRet >= dtFirst.AddMonths(1))
                dtRet = dtRet.AddDays(-7);
            //return
            return dtRet;
        }
    }
}

