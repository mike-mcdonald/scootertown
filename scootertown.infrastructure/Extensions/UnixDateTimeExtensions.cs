using System;

namespace PDX.PBOT.Scootertown.Infrastructure.Extensions
{
    public static class UnixDateTimeExtensions
    {
        private static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

        public static DateTime ToDateTime(this int timestamp)
        {
            var datetime = ((long?)timestamp).ToDateTime();
            return datetime.Value;
        }

        public static DateTime? ToDateTime(this int? timestamp)
        {
            var datetime = ((long?)timestamp).ToDateTime();
            return datetime;
        }

        public static DateTime ToDateTime(this long timestamp)
        {
            var datetime = ((long?)timestamp).ToDateTime();
            return datetime.Value;
        }

        public static DateTime? ToDateTime(this long? timestamp)
        {
            if (!timestamp.HasValue)
            {
                return (DateTime?)null;
            }

            var datetime = FromUnixTimestamp(timestamp.Value).ToLocalTime();
            return datetime;
        }

        public static long ToUnixTimestamp(this DateTime d)
        {
            var timeSinceEpoch = d - UnixEpoch;

            return (long)timeSinceEpoch.TotalSeconds;
        }

        private static DateTime FromUnixTimestamp(long timestamp)
        {
            return UnixEpoch.AddSeconds(timestamp);
        }
    }
}
