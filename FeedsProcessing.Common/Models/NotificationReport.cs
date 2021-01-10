using System.Text.Json;

namespace FeedsProcessing.Common.Models
{
    public class NotificationReport
    {
        public string Id { get; set; }
        public int WordCount { get; set; }

        public NotificationSource Source { get; set; }

        /// <summary>
        /// Serializes object into JSON string 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => JsonSerializer.Serialize(this, Serialization.SerializerOptions);
    }
}
