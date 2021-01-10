using FeedsProcessing.Common.Models;
using FeedsProcessing.Dal;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FeedsProcessing.Logic
{

    public interface IReportCalculator
    {
        public Task<ReportResponse> Calculate(Notification notification);
    }


    public class ReportCalculator : IReportCalculator
    {
        private readonly IReportCalculationDal _reportsDal;

        public ReportCalculator(IReportCalculationDal reportsDal)
        {
            _reportsDal = reportsDal;
        }

        public async Task<ReportResponse> Calculate(Notification notification)
        {
            string[] contents;

            switch (notification)
            {
                case TwitterNotification twitter:
                    contents = GetTextContents(twitter.Tweets, "text");
                    break;

                case FacebookNotification facebook:
                    contents = GetTextContents(facebook.Posts, "content");
                    break;
                default:
                    throw new InvalidOperationException("Unable to calculate report");
            }

            if (contents?.Any() == false)
                return ReportResponse.Empty;

            var request = new ReportRequest { Contents = contents };
            return await _reportsDal.Calculate(request);
        }

        private static string[] GetTextContents(JsonElement elm, string propName) =>
            elm.EnumerateArray()
                .Select(j => j.TryGetProperty(propName, out var text) ? text.GetString() : null)
                .Where(t => t != null)
                .ToArray();
    }
}
