namespace FeedsProcessing.Common.Models
{
    public class ReportResponse
    {
        public static ReportResponse Empty = new ReportResponse();
        public int WordCount { get; set; }
    }
}
