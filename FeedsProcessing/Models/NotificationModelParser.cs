using FeedsProcessing.Common;
using FeedsProcessing.Common.Models;
using System;
using System.Text.Json;

namespace FeedsProcessing.Models
{
    public interface INotificationModelParser
    {
        public ModelResult<NotificationModel> Parse(JsonElement json);
    }

    public class NotificationModelParser : INotificationModelParser
    {
        public ModelResult<NotificationModel> Parse(JsonElement json)
        {
            var source = TryParseNotificationSource(json);

            NotificationModel model;
            switch (source)
            {
                case NotificationSource.Facebook:
                    model = JsonSerializer.Deserialize<FacebookNotificationModel>(json.GetRawText(), Serialization.SerializerOptions);
                    break;
                case NotificationSource.Twitter:
                    model = JsonSerializer.Deserialize<TwitterNotificationModel>(json.GetRawText(), Serialization.SerializerOptions);
                    break;
                default:
                    return ModelResult<NotificationModel>.Fail("Failed to parse notification source specified");
            }

            return ModelResult<NotificationModel>.Ok(model);


        }

        private static NotificationSource TryParseNotificationSource(JsonElement json) =>
            json.TryGetProperty("source", out var sourceStr) &&
            Enum.TryParse<NotificationSource>(sourceStr.GetString(), true, out var source)
                ? source
                : NotificationSource.None;
    }
}