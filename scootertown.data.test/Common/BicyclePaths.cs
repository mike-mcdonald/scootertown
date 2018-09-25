using System;
using System.Collections.Generic;
using PDX.PBOT.Scootertown.Data.Models.Dimensions;
using PDX.PBOT.Scootertown.Data.Models.Facts;

namespace PDX.PBOT.Scootertown.Data.Tests.Common
{
    public class BicyclePaths
    {
        readonly List<BicyclePath> Segments = new List<BicyclePath>();

        public BicyclePaths()
        {
            for (int i = 0; i < Length; i++)
            {
                Segments.Add(new BicyclePath
                {
                    Name = $"ABC{i}",
                    AlternateKey = $"{i}",
                    X = i,
                    Y = i,
                    Buffer = 100
                });
            }
        }

        public byte Length
        {
            get
            {
                return 2;
            }
        }

        public BicyclePath this[int index]    // Indexer declaration  
        {
            get
            {
                return Segments[index];
            }
        }
    }
}
