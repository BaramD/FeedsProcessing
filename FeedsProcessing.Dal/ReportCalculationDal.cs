using FeedsProcessing.Common;
using FeedsProcessing.Common.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FeedsProcessing.Dal
{
    public interface IReportCalculationDal
    {
        public Task<ReportResponse> Calculate(ReportRequest request);
    }

    public class ReportCalculationDal : IReportCalculationDal
    {

        //TODO: move requestUri value to appsettings.json
        private const string RequestUri = "https://localhost:44377/api/reports/summary";


        //Consider using IHttpClientFactory to implement resilient HTTP requests:
        // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        private static readonly HttpClient Client = new HttpClient();

        public async Task<ReportResponse> Calculate(ReportRequest request)
        {
            try
            {
                using var content = new StringContent(
                                        JsonSerializer.Serialize(request, Serialization.SerializerOptions),
                                        Encoding.UTF8,
                                        "application/json");

                using var response = await Client.PostAsync(RequestUri, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ReportResponse>(responseBody, Serialization.SerializerOptions);
            }
            catch (HttpRequestException e)
            {
                throw new InvalidOperationException(e.StackTrace);
            }
        }
    }
}
