using System.Text.Json;

namespace FeedsProcessing.Common.Models
{
    public class FacebookNotification : Notification
    {
        public JsonElement Posts { get; set; }
    }
}
