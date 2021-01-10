using FeedsProcessing.Common;
using FeedsProcessing.Common.Models;
using FeedsProcessing.Dal.Utils;
using System.IO;
using System.Threading.Tasks;

namespace FeedsProcessing.Dal
{
    public interface INotificationReportDal
    {
        public Task Save(BackingStoreInfo stateInfo, string data);
    }

    public class NotificationReportDal : INotificationReportDal
    {
        public async Task Save(BackingStoreInfo info, string data)
        {
            var filePath = FilePathBuilder.Build(info, Constants.SummaryFileNameFormat);
            await File.WriteAllTextAsync(filePath, data);
        }
    }
}
