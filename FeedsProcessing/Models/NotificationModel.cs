using FeedsProcessing.Common.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FeedsProcessing.Models
{
    public abstract class NotificationModel
    {
        public string Id { get; set; }
        public string Token { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        public abstract Notification FromModel();

    }

    public class TwitterNotificationModel : NotificationModel
    {
        public JsonElement Tweets { get; set; }

        public override Notification FromModel() =>
            new TwitterNotification
            {
                Id = Id,
                Token = Token,
                CreatedAt = CreatedAt,
                Source = NotificationSource.Twitter,
                Tweets = Tweets
            };
    }

    public class FacebookNotificationModel : NotificationModel
    {
        public JsonElement Posts { get; set; }

        public override Notification FromModel() =>
            new FacebookNotification
            {
                Id = Id,
                Token = Token,
                CreatedAt = CreatedAt,
                Source = NotificationSource.Facebook,
                Posts = Posts
            };
    }



}
