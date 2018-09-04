using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class Companies
    {
        readonly List<Company> _company = new List<Company>();

        public Companies()
        {
            var baseDate = new DateTime(2018, 8, 13);

            for (int i = 0; i < Length; i++)
            {
                _company.Add(new Company
                {
                    Name = $"ABC{i}"
                });
            }
        }

        public byte Length
        {
            get
            {
                return 5;
            }
        }

        public Company this[int index]    // Indexer declaration  
        {
            get
            {
                return _company[index];
            }
        }
    }
}
