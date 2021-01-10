using System.Linq;
using FeedsProcessing.Common.Models;
using FeedsReporting.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FeedsReporting.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly ISummaryCalculator _summaryCalculator;

        public ReportsController(ILogger<ReportsController> logger, ISummaryCalculator summaryCalculator)
        {
            _logger = logger;
            _summaryCalculator = summaryCalculator;
        }

        [HttpPost]
        [Route("api/reports/summary")]
        public IActionResult GetSummary(ReportRequest request)
        {
            _logger.LogInformation("About to calculate report");
            if (request == null)
                return BadRequest("Invalid parameter 'request' ");

            if (request.Contents == null)
                return BadRequest("Invalid parameter 'request.contents' ");

            var count = request.Contents
                                   .Select(c => _summaryCalculator.CalculateWordCount(c))
                                   .Sum();

            return Ok(new ReportResponse { WordCount = count });
        }
    }
}
