using FeedsProcessing.Common;
using FeedsProcessing.Common.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace FeedsProcessing.Dal
{
    public class ProcessingState
    {
        //New Asp.NET Core 3.0 Json doesn't serialize Dictionary<Key,Value> with Enum key
        //https://github.com/dotnet/runtime/issues/30524
        public Dictionary<string, int> Indices { get; set; } = new Dictionary<string, int>
        {
            [Key(NotificationSource.Twitter)] = 0,
            [Key(NotificationSource.Facebook)] = 0
        };

        /// <summary>
        /// Serializes object into JSON string 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => JsonSerializer.Serialize(this, Serialization.SerializerOptions);

        /// <summary>
        /// Deserializes the JSON the object 
        /// </summary>
        /// <returns></returns>
        public static ProcessingState FromString(string value) => JsonSerializer.Deserialize<ProcessingState>(value, Serialization.SerializerOptions);

        public void IncrementIndex(NotificationSource source) => Indices[Key(source)]++;

        public int GetIndex(NotificationSource source) => Indices[Key(source)];

        private static string Key(NotificationSource source) => source.ToString().ToLowerInvariant();
    }
}