using System.Text.Json;

namespace FeedsProcessing.Common.Models
{
    public class TwitterNotification : Notification
    {
        public JsonElement Tweets { get; set; }
    }
}