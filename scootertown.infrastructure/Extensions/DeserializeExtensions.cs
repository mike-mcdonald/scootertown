using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PDX.PBOT.Scootertown.Infrastructure.JSON;

namespace PDX.PBOT.Scootertown.Infrastructure.Extensions
{
    public static class DeserializeExtensions
    {
        public static async Task<T> DeserializeJson<T>(this HttpResponseMessage response, T anonymousObject)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(stream));
            var serializer = JsonSerializer.Create();
            serializer.Converters.Add(new SafeGeoJsonConverter());
            return serializer.Deserialize<T>(reader);
        }
    }
}
