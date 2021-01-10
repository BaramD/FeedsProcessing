using System;

namespace FeedsReporting.Logic
{
    public interface ISummaryCalculator
    {
        int CalculateWordCount(string content);
    }

    public class SummaryCalculator : ISummaryCalculator
    {
        public int CalculateWordCount(string content) =>
            content
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Length;
    }
}
