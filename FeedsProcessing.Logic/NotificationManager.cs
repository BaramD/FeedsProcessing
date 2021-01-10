using FeedsProcessing.Common.Models;
using FeedsProcessing.Common.Utils;
using FeedsProcessing.Dal;
using System.Text.Json;
using System.Threading.Tasks;


namespace FeedsProcessing.Logic
{
    public interface INotificationManager
    {
        public Task Save(Notification notification, JsonElement json);
    }

    public class NotificationManager : INotificationManager
    {
        private readonly INotificationDal _notificationDal;
        private readonly INotificationReportDal _reportDal;
        private readonly IProcessingStateDal _processingStateDal;
        private readonly IReportCalculator _reportCalculator;

        public NotificationManager(
            INotificationDal notificationDal,
            IReportCalculator reportCalculator,
            IProcessingStateDal processingStateDal,
            INotificationReportDal reportDal)
        {
            _notificationDal = notificationDal;
            _reportCalculator = reportCalculator;
            _processingStateDal = processingStateDal;
            _reportDal = reportDal;
        }

        public async Task Save(Notification notification, JsonElement json)
        {
            BackingStoreInfo storeInfo = await GetBackingStoreInfo(notification);

            await _notificationDal.Save(storeInfo, json.GetRawText());
            await CalculateAndSaveReport(notification, storeInfo);
        }

        private async Task CalculateAndSaveReport(Notification notification, BackingStoreInfo stateInfo)
        {
            var summary = await _reportCalculator.Calculate(notification);
            var report = new NotificationReport
            {
                Source = notification.Source,
                Id = notification.Id,
                WordCount = summary.WordCount
            };
            await _reportDal.Save(stateInfo, report.ToString());
        }

        private async Task<BackingStoreInfo> GetBackingStoreInfo(Notification notification)
        {
            var dirIndex = await _processingStateDal.GetIndex(notification.Source);
            var fileId = RandomUtils.String();
            return new BackingStoreInfo(notification.Source, dirIndex, fileId);
        }
    }
}