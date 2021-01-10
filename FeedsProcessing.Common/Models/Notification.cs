using System.Text.Json.Serialization;

namespace FeedsProcessing.Common.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public string Token { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        public NotificationSource Source { get; set; }

    }
}