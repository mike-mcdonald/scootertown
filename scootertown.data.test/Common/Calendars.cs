using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class Calendars
    {
        readonly List<Calendar> _calendar = new List<Calendar>();

        public Calendars()
        {
            var baseDate = new DateTime(2018, 8, 13);

            for (int i = 0; i < Length; i++)
            {
                _calendar.Add(new Calendar
                {
                    Date = baseDate.AddDays(i)
                });
            }
        }

        public byte Length
        {
            get
            {
                return 10;
            }
        }

        public Calendar this[int index]    // Indexer declaration  
        {
            get
            {
                return _calendar[index];
            }
        }
    }
}
