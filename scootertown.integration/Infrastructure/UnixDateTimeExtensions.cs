using System;

namespace PDX.PBOT.Scootertown.Integration.Infrastructure
{
    public static class UnixDateTimeExtensions
    {
        private static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        public static long ToUnixTimestamp(this DateTime d)
        {
            var timeSinceEpoch = d - UnixEpoch;

            return (long)timeSinceEpoch.TotalSeconds;
        }

        public static DateTime FromUnixTimestamp(this DateTime d, long timestamp)
        {
            return UnixEpoch.AddSeconds(timestamp);
        }
    }
}
