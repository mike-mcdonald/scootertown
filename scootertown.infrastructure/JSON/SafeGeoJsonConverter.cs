using Newtonsoft.Json;

namespace PDX.PBOT.Scootertown.Infrastructure.JSON
{
    public class SafeGeoJsonConverter : GeoJSON.Net.Converters.GeoJsonConverter
    {
        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                return existingValue;
            }
        }
    }
}
