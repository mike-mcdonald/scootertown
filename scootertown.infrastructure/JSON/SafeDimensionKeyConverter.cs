using System;
using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.Infrastructure.JSON
{
    public class SafeDimensionKeyConverter : JsonConverter
    {
        public SafeDimensionKeyConverter()
        {
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            var value = Convert.ToInt32(reader.Value);

            return value != 0 ? value : (int?)null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
