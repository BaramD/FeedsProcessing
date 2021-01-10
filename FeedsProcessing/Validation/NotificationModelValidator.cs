using FeedsProcessing.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FeedsProcessing.Validation
{
    public interface INotificationModelValidator
    {
        public ValidationResult Validate(NotificationModel model);
    }

    public class NotificationModelValidator : INotificationModelValidator
    {
        public ValidationResult Validate(NotificationModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
                return new ValidationResult("Invalid parameter 'id' specified");

            if (string.IsNullOrEmpty(model.Token))
                return new ValidationResult("Invalid parameter 'token' specified");

            if (!IsValidDateTime(model.CreatedAt))
                return new ValidationResult("Invalid parameter 'created_at' specified");

            switch (model)
            {
                case FacebookNotificationModel facebook:
                    if (facebook.Posts.ValueKind != JsonValueKind.Array)
                        return new ValidationResult("Invalid parameter 'posts' specified");
                    break;
                case TwitterNotificationModel twitter:
                    if (twitter.Tweets.ValueKind != JsonValueKind.Array)
                        return new ValidationResult("Invalid parameter 'tweets' specified");
                    break;
            }

            return ValidationResult.Success;
        }

        //TODO: should be extension method
        private static bool IsValidDateTime(string date) =>
            !string.IsNullOrEmpty(date) && DateTime.TryParse(date, out _);
    }
}
