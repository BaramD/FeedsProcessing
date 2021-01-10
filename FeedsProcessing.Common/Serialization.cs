using System.Text.Json;
using System.Text.Json.Serialization;

namespace FeedsProcessing.Common
{
    public static class Serialization
    {
        public static JsonSerializerOptions SerializerOptions = CreateJsonSerializerOptions();

        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

    }
}
